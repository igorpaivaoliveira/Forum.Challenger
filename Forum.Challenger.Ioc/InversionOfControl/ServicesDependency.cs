using Microsoft.Extensions.DependencyInjection;
using Forum.Challenger.Domain.Contracts.Repositories.EntityFramework;
using Forum.Challenger.Infra.Repositories.EntityFramework;
using Forum.Challenger.Domain.Commands.Input;
using Forum.Challenger.Domain.Handler;
using Forum.Challenger.Domain.Commands.Output;
using MediatR;

namespace Forum.Challenger.Ioc.InversionOfControl
{
    public static class ServicesDependency
    {
        public static IServiceCollection AddServicesDependency(this IServiceCollection services)
        {
            #region Repositories

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            #endregion           

            #region Handlers

            //Person
            services.AddScoped<IRequestHandler<PersonsCreateCommand, Response>, PersonsCreateHandler>();
            services.AddScoped<IRequestHandler<PersonsLoginCommand, Response>, PersonsLoginHandler>();

            //Topic
            services.AddScoped<IRequestHandler<TopicsFirstCommand, Response>, TopicsFirstHandler>();
            services.AddScoped<IRequestHandler<TopicsListCommand, Response>, TopicsListHandler>();
            services.AddScoped<IRequestHandler<TopicsUpdateCommand, Response>, TopicsUpdateHandler>();
            services.AddScoped<IRequestHandler<TopicsCreateCommand, Response>, TopicsCreateHandler>();
            services.AddScoped<IRequestHandler<VwTopicDetailsListCommand, Response>, VwTopicDetailsListHandler>();

            #endregion

            #region Configuration

            #endregion

            return services;
        }
    }
}
