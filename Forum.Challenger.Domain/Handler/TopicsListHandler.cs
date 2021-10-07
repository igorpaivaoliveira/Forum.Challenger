using Flunt.Notifications;
using Forum.Challenger.Domain.Commands.Input;
using Forum.Challenger.Domain.Commands.Output;
using Forum.Challenger.Domain.Contracts.Repositories.EntityFramework;
using Forum.Challenger.Domain.Extensions;
using Forum.Challenger.Domain.Model.Entity;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Forum.Challenger.Domain.Handler
{
    public class TopicsListHandler : IRequestHandler<TopicsListCommand, Response>, IDisposable
    {
        IUnitOfWork _unitOfWork;

        public TopicsListHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Response> Handle(TopicsListCommand request, CancellationToken cancellationToken)
        {
            var response = new Response();

            try
            {
                var topic = await _unitOfWork.TopicsEFRepository.ListAsync();

                if (topic == null)
                    response.AddNotification(new Notification("400", $"Not exist topic."));
                else
                    response.AddValue(topic);

            }
            catch(Exception ex)
            {
                response.AddNotification(new Notification("400", $"Error when get the topics. Ex: {ex.Message}"));
            }

            return response;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
