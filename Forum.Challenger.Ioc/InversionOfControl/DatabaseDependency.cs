using Forum.Challenger.Transaction.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Forum.Challenger.Ioc.InversionOfControl
{
    public static class DatabaseDependency
    {
        public static IServiceCollection AddDatabaseDependency(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration["ConnectionString"];

            if (!int.TryParse(configuration["DbTimeOut"], out int timeOut)) timeOut = 30;

            services.AddDbContext<ForumChallengerDbContext>(options =>
            {
                options.UseSqlServer(connectionString, sqlServerOptions => sqlServerOptions.CommandTimeout(timeOut));
            });

            return services;
        }
    }
}
