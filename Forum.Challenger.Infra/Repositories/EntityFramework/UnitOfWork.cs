using Forum.Challenger.Domain.Contracts.Repositories.EntityFramework;
using Forum.Challenger.Transaction.Infra.Data.Context;

namespace Forum.Challenger.Infra.Repositories.EntityFramework
{
    public class UnitOfWork : BaseEFRepository<object>, IUnitOfWork
    {
        public IPersonsEFRepository PersonsEFRepository { get; }

        public ITopicsEFRepository TopicsEFRepository { get; }

        public IVwTopicsDetailsEFRepository VwTopicsDetailsEFRepository { get; }

        public UnitOfWork(ForumChallengerDbContext dbContext) : base(dbContext)
        {
            PersonsEFRepository = new PersonsEFRepository(dbContext);
            TopicsEFRepository = new TopicsEFRepository(dbContext);
            VwTopicsDetailsEFRepository = new VwTopicsDetailsEFRepository(dbContext);
        }
    }
}
