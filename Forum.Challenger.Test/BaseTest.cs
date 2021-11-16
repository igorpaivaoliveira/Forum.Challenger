using Forum.Challenger.Domain.Commands.Input;
using Forum.Challenger.Domain.Commands.Output;
using Forum.Challenger.Domain.Contracts.Repositories.EntityFramework;
using Forum.Challenger.Domain.Handler;
using Forum.Challenger.Infra.Repositories.EntityFramework;
using Forum.Challenger.Transaction.Infra.Data.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Forum.Challenger.Test
{
    public class BaseTest : IDisposable
    {

        public IMediator mediator { get; }
        public IServiceCollection services { get; }

        public IConfiguration configuration { get; }

        public BaseTest(IMediator mediator, IServiceCollection services, IConfiguration configuration)
        {
            this.mediator = mediator;
            this.services = services;

            string connectionString = configuration["ConnectionString"];

            if (!int.TryParse(configuration["DbTimeOut"], out int timeOut)) timeOut = 30;

            services.AddDbContext<ForumChallengerDbContext>(options =>
            {
                options.UseSqlServer(connectionString, sqlServerOptions => sqlServerOptions.CommandTimeout(timeOut));
            });

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            //Person
            services.AddScoped<IRequestHandler<PersonsCreateCommand, Response>, PersonsCreateHandler>();
            services.AddScoped<IRequestHandler<PersonsLoginCommand, Response>, PersonsLoginHandler>();

            //Topic
            services.AddScoped<IRequestHandler<TopicsFirstCommand, Response>, TopicsFirstHandler>();
            services.AddScoped<IRequestHandler<TopicsListCommand, Response>, TopicsListHandler>();
            services.AddScoped<IRequestHandler<TopicsUpdateCommand, Response>, TopicsUpdateHandler>();
            services.AddScoped<IRequestHandler<TopicsCreateCommand, Response>, TopicsCreateHandler>();
            services.AddScoped<IRequestHandler<VwTopicDetailsListCommand, Response>, VwTopicDetailsListHandler>();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
