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
    public class PersonsLoginHandler : IRequestHandler<PersonsLoginCommand, Response>, IDisposable
    {
        IUnitOfWork _unitOfWork;

        public PersonsLoginHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Response> Handle(PersonsLoginCommand request, CancellationToken cancellationToken)
        {
            var response = new Response();

            try
            {
                var user = _unitOfWork.PersonsEFRepository.FirstNoTracking(x => x.DsEmail == request.DsEmail && x.DsPassword == request.DsPassword);

                if (user == null)
                {
                    response.AddNotification(new Notification("400", "User not found, please check your login or pass"));
                    return response;
                }

                response.AddValue(user);

            }
            catch(Exception ex)
            {
                
                response.AddNotification(new Notification("400", $"Ex: {ex.Message}"));
            }

            return response;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
