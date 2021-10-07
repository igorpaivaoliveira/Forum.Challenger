using Forum.Challenger.Domain.Model.Entity;
using Forum.Challenger.Infra.Mapping;
using Microsoft.EntityFrameworkCore;

namespace Forum.Challenger.Transaction.Infra.Data.Context
{
    public class ForumChallengerDbContext : DbContext
    {
        public virtual DbSet<Persons> Persons { get; set; }

        public ForumChallengerDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PersonsMapping).Assembly);

        }
    }
}
