using System;

namespace Forum.Challenger.Domain.Contracts.Repositories.EntityFramework
{
    public interface IUnitOfWork : IBaseEFRepository<object>
    {
        IPersonsEFRepository PersonsEFRepository { get; }

        ITopicsEFRepository TopicsEFRepository { get; }
    }
}
