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
    public class TopicsCreateHandler : IRequestHandler<TopicsCreateCommand, Response>, IDisposable
    {
        IUnitOfWork _unitOfWork;

        public TopicsCreateHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Response> Handle(TopicsCreateCommand request, CancellationToken cancellationToken)
        {
            var response = new Response();

            try
            {
                var topic = new Topics().SetPropertyAutomap(request);

                await _unitOfWork.TopicsEFRepository.AddAsync(topic);

                await _unitOfWork.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                response.AddNotification(new Notification("400", $"Error when registering the topic. Ex: {ex.Message}"));
            }

            return response;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
