using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ChallengeBank.Infrastructure.Data.Factories
{
    public class ChallengeBankContextFactory : IDesignTimeDbContextFactory<ChallengeBankContext>
    {
        public ChallengeBankContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ChallengeBankContext>();
            optionsBuilder.UseSqlite("Data Source=challengebank.db");

            return new ChallengeBankContext(optionsBuilder.Options);
        }
    }
}
