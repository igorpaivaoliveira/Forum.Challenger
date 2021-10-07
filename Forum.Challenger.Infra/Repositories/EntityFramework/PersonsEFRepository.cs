using Forum.Challenger.Domain.Contracts.Repositories.EntityFramework;
using Forum.Challenger.Domain.Model.Entity;
using Forum.Challenger.Transaction.Infra.Data.Context;

namespace Forum.Challenger.Infra.Repositories.EntityFramework
{
    public class PersonsEFRepository : BaseEFRepository<Persons>, IPersonsEFRepository
    {
        public PersonsEFRepository(ForumChallengerDbContext context) : base(context)
        {
        }
    }
}
