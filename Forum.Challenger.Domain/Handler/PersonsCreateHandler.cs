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
    public class PersonsCreateHandler : IRequestHandler<PersonsCreateCommand, Response>, IDisposable
    {
        IUnitOfWork _unitOfWork;

        public PersonsCreateHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Response> Handle(PersonsCreateCommand request, CancellationToken cancellationToken)
        {
            var response = new Response();

            try
            {
                var personExist = _unitOfWork.PersonsEFRepository.FirstNoTracking(x => x.DsEmail == request.DsEmail);

                if (personExist != null)
                {
                    response.AddNotification(new Notification("400", "Email already registered"));
                    return response;
                }

                var person = new Persons().SetPropertyAutomap(request);

                await _unitOfWork.PersonsEFRepository.AddAsync(person);

                await _unitOfWork.SaveChangesAsync();

                person = _unitOfWork.PersonsEFRepository.FirstNoTracking(x => x.DsEmail == person.DsEmail);

                response.AddValue(person);

            }
            catch(Exception ex)
            {
                
                response.AddNotification(new Notification("400", $"Error when registering the user. Ex: {ex.Message}"));
            }

            return response;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
