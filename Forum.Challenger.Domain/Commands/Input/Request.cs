using Flunt.Notifications;
using MediatR;

namespace Forum.Challenger.Domain.Commands.Input
{
    public abstract class Request<TResponse> : IRequest<TResponse>, IBaseRequest
    {
        public abstract void Validate();
    }
}