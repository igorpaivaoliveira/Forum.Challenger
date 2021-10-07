using Forum.Challenger.Domain.Contracts.Repositories.EntityFramework;
using Forum.Challenger.Domain.Model.Entity;
using Forum.Challenger.Transaction.Infra.Data.Context;

namespace Forum.Challenger.Infra.Repositories.EntityFramework
{
    public class TopicsEFRepository : BaseEFRepository<Topics>, ITopicsEFRepository
    {
        public TopicsEFRepository(ForumChallengerDbContext context) : base(context)
        {
        }
    }
}
