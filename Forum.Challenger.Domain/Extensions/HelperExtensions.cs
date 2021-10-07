using Serilog.Events;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Forum.Challenger.Domain.Extensions
{
    public static class HelperExtensions
    {
        /// <summary>
        /// Remover Caracteres especiais
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string RemoveControlAndSpecialCharacters(this string str)
        {
            /** Troca os caracteres acentuados por não acentuados **/
            string[] acentos = new string[] { "ç", "Ç", "á", "é", "í", "ó", "ú", "ý", "Á", "É", "Í", "Ó", "Ú", "Ý", "à", "è", "ì", "ò", "ù", "À", "È", "Ì", "Ò", "Ù", "ã", "õ", "ñ", "ä", "ë", "ï", "ö", "ü", "ÿ", "Ä", "Ë", "Ï", "Ö", "Ü", "Ã", "Õ", "Ñ", "â", "ê", "î", "ô", "û", "Â", "Ê", "Î", "Ô", "Û" };
            string[] semAcento = new string[] { "c", "C", "a", "e", "i", "o", "u", "y", "A", "E", "I", "O", "U", "Y", "a", "e", "i", "o", "u", "A", "E", "I", "O", "U", "a", "o", "n", "a", "e", "i", "o", "u", "y", "A", "E", "I", "O", "U", "A", "O", "N", "a", "e", "i", "o", "u", "A", "E", "I", "O", "U" };

            for (int i = 0; i < acentos.Length; i++)
            {
                str = str.Replace(acentos[i], semAcento[i]);
            }
            /** Troca os caracteres especiais da string por "" **/
            string[] caracteresEspeciais = { "¹", "²", "³", "£", "¢", "¬", "º", "¨", "\"", "'", ".", ",", "-", ":", "(", ")", "ª", "|", "\\\\", "°", "_", "@", "#", "!", "$", "%", "&", "*", ";", "/", "<", ">", "?", "[", "]", "{", "}", "=", "+", "§", "´", "`", "^", "~" };

            for (int i = 0; i < caracteresEspeciais.Length; i++)
            {
                str = str.Replace(caracteresEspeciais[i], "");
            }

            /** Troca os caracteres especiais da string por " " **/
            str = Regex.Replace(str, @"[^\w\.@-]", " ",
                                RegexOptions.None, TimeSpan.FromSeconds(1.5));

            return str.Trim();
        }
        /// <summary>
        /// Obter o valor default do objeto
        /// </summary>
        /// <param name="type">Objeto para retorno do tipo padrão</param>
        /// <returns></returns>
        public static object GetDefault(this Type type)
        {
            // If no Type was supplied, if the Type was a reference type, or if the Type was a System.Void, return null
            if (type == null || !type.IsValueType || type == typeof(void))
                return null;

            // If the supplied Type has generic parameters, its default value cannot be determined
            if (type.ContainsGenericParameters)
                throw new ArgumentException(
                    "{" + MethodInfo.GetCurrentMethod() + "} Error:\n\nThe supplied value type <" + type +
                    "> contains generic parameters, so the default value cannot be retrieved");

            // If the Type is a primitive type, or if it is another publicly-visible value type (i.e. struct), return a 
            //  default instance of the value type
            if (type.IsPrimitive || !type.IsNotPublic)
            {
                try
                {
                    return Activator.CreateInstance(type);
                }
                catch (Exception e)
                {
                    throw new ArgumentException(
                        "{" + MethodInfo.GetCurrentMethod() + "} Error:\n\nThe Activator.CreateInstance method could not " +
                        "create a default instance of the supplied value type <" + type +
                        "> (Inner Exception message: \"" + e.Message + "\")", e);
                }
            }

            // Fail with exception
            throw new ArgumentException("{" + MethodInfo.GetCurrentMethod() + "} Error:\n\nThe supplied value type <" + type +
                "> is not a publicly-visible type, so the default value cannot be retrieved");
        }
        public static T GetDefault<T>()
        {
            return (T)GetDefault(typeof(T));
        }
        /// <summary>
        /// Obter dados da propriedade com retorno em uma lista
        /// </summary>
        /// <param name="obj">objeto a ser retornado</param>
        /// <returns></returns>
        public static Dictionary<string, object> GetPropertyDictionary(this object obj)
        {
            var propDictionary = new Dictionary<string, object>();

            var passedType = obj.GetType();

            foreach (var propertyInfo in passedType.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).Where(p => p.CanRead && p.CanWrite)?.ToArray())
            {
                var value = propertyInfo.GetValue(obj, null);
                if (value != null)
                    propDictionary.Add(propertyInfo.Name, value);
            }

            return propDictionary;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T">Tipo</typeparam>
        /// <param name="obj">Objeto a ser atualizado</param>
        /// <param name="objUpdate">Objeto a ser comparado</param>
        /// <returns></returns>
        public static T SetPropertyAutomap<T>(this T obj, object objUpdate) where T : class
        {
            var instance = Activator.CreateInstance(obj.GetType());
            var propertiesIntance = instance.GetPropertyDictionary();
            var passedType = obj.GetType();
            var passedUpdateType = objUpdate.GetPropertyDictionary();
            foreach (var propertyInfo in passedType.GetProperties())
            {
                try
                {
                    var name = propertyInfo.Name?.ToString();
                    var prop = GetDefault(propertyInfo.GetType());
                    var properttype = propertyInfo.GetType();
                    var value = passedUpdateType.FirstOrDefault(x => x.Key == name).Value;

                    if (value == null && passedUpdateType.Any(x => !x.Value.GetType().IsSimpleType() && x.Key.Contains(name) && propertyInfo?.PropertyType.Name.ToUpper() == x.Value?.GetType().Name.ToUpper()))
                        value = passedUpdateType.FirstOrDefault(x => x.Key.StartsWith(name) && propertyInfo?.PropertyType.Name.ToUpper() == x.Value?.GetType().Name.ToUpper()).Value;

                    var defaultValue = propertiesIntance.FirstOrDefault(x => x.Key == propertyInfo.Name);

                    if ((value != null && value != defaultValue.Value) && (propertyInfo.CanWrite && propertyInfo.CanRead) &&
                        (propertyInfo?.PropertyType.Name.ToUpper() == value?.GetType().Name.ToUpper() ||
                        (propertyInfo?.PropertyType?.GenericTypeArguments.Length > 0 && propertyInfo?.PropertyType?.GenericTypeArguments.FirstOrDefault().FullName.ToUpper() == value?.GetType().FullName.ToUpper())
                        ))
                    {
                        propertyInfo.SetValue(obj, value);
                    }
                    else if ((value != null && value != defaultValue.Value) && !value.GetType().IsSimpleType() && propertyInfo.PropertyType.Name.ToUpper().Contains(value.GetType().Name.ToUpper()))
                    {
                        if (propertyInfo is IEnumerable)
                        {
                            IList list = (IList)Activator.CreateInstance(propertyInfo.PropertyType);
                            var enumerator = ((IEnumerable)value).GetEnumerator();
                            while (enumerator.MoveNext())
                            {
                                list.Add(enumerator.Current);
                            }
                            propertyInfo.SetValue(obj, list);
                        }
                        else
                            propertyInfo.SetValue(obj, value);
                    }
                }
                catch (Exception erro)
                {
                    var system = new { message = erro.Message, logEventLevel = LogEventLevel.Error, erro = erro };
                }

            }
            return obj;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T">Tipo do objeto para junção da expression</typeparam>
        /// <param name="filter1">Expression existente</param>
        /// <param name="filter2">Expression a ser unida</param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> Combine<T>(this Expression<Func<T, bool>> filter1, Expression<Func<T, bool>> filter2)
        {
            var rewrittenBody1 = new ReplaceVisitor(
                filter1.Parameters[0], filter2.Parameters[0]).Visit(filter1.Body);
            var newFilter = Expression.Lambda<Func<T, bool>>(
                Expression.AndAlso(rewrittenBody1, filter2.Body), filter2.Parameters);
            return newFilter;
        }

        /// <summary>
        /// Obtem uma expression a partir de um objeto 
        /// </summary>
        /// <typeparam name="T">Tipo do Objeto</typeparam>
        /// <param name="obj">Objeto com os seus valores</param>
        /// <example>var filter = product.ToFilter<Biz.Base.Domain.Model.Entity.Product>();</example>
        /// <returns></returns>
        public static Expression<Func<T, bool>> ToFilter<T>(this object obj)
        {
            var filterList = obj.GetPropertyDictionary();
            Expression<Func<T, bool>> custom = x => true;
            foreach (var item in filterList)
            {
                if (item.Value?.ToString() == GetDefault(item.Value?.GetType())?.ToString())
                    continue;
                var parameter = Expression.Parameter(typeof(T), "x");
                MemberExpression member = Expression.Property(parameter, item.Key);
                ConstantExpression constant = Expression.Constant(item.Value);
                Expression body = null;
                if (member.Type.IsNullableType())
                {
                    var filter1 = Expression.Constant(Convert.ChangeType(item.Value, member.Type.GetGenericArguments()[0]));
                    Expression typeFilter = Expression.Convert(filter1, member.Type);
                    body = Expression.Equal(member, typeFilter);
                }
                else
                {
                    body = Expression.Equal(member, constant);
                }

                var finalExpression = Expression.Lambda<Func<T, bool>>(body, parameter);
                custom = custom.Combine(finalExpression);
            }
            return custom;
        }
        public static bool IsNullableType(this Type type)
        {
            return type.IsGenericType && (type.GetGenericTypeDefinition().Equals(typeof(Nullable<>)));
        }

        public static bool IsSimpleType(this Type type)
        {
            return
                type.IsPrimitive ||
                new Type[] {
            typeof(string),
            typeof(decimal),
            typeof(DateTime),
            typeof(DateTimeOffset),
            typeof(TimeSpan),
            typeof(Guid)
                }.Contains(type) ||
                type.IsEnum ||
                Convert.GetTypeCode(type) != TypeCode.Object ||
                type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>) && type.GetGenericArguments()[0].IsSimpleType()
                ;
        }
    }
}
