
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
    public class VwTopicDetailsListHandler : IRequestHandler<VwTopicDetailsListCommand, Response>, IDisposable
    {
        IUnitOfWork _unitOfWork;

        public VwTopicDetailsListHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Response> Handle(VwTopicDetailsListCommand request, CancellationToken cancellationToken)
        {
            var response = new Response();

            try
            {
                var topicDetails = await _unitOfWork.VwTopicsDetailsEFRepository.ListAsync();

                if (topicDetails == null)
                    response.AddNotification(new Notification("400", $"Not exist topics."));
                else
                    response.AddValue(topicDetails);

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
