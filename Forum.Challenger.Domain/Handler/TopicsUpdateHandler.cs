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
    public class TopicsUpdateHandler : IRequestHandler<TopicsUpdateCommand, Response>, IDisposable
    {
        IUnitOfWork _unitOfWork;

        public TopicsUpdateHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Response> Handle(TopicsUpdateCommand request, CancellationToken cancellationToken)
        {
            var response = new Response();

            try
            {
                var topic = _unitOfWork.TopicsEFRepository.FirstNoTracking(x => x.CdTopic == request.CdTopic);

                topic.DtTopicLastEdition = DateTime.Now;
                topic.DsTitle = request.DsTitle;
                topic.DsText = request.DsText;
                topic.FlActive = request.FlActive;

                _unitOfWork.TopicsEFRepository.Update(topic);

                await _unitOfWork.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                response.AddNotification(new Notification("400", $"Error when updating the topic. Ex: {ex.Message}"));
            }

            return response;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
