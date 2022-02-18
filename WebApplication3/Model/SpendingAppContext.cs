using Microsoft.EntityFrameworkCore;

namespace WebApplication3.Model
{
    public class SpendingAppDbContext : DbContext
    {


        public SpendingAppDbContext(DbContextOptions<SpendingAppDbContext> options) : base(options)
        {

        }



        public DbSet<PwRecoveryToken> PwRecoveryToken { get; set; }

        public DbSet<User> User { get; set; }

        public DbSet<AccountVerificationToken> AccountVerificationToken { get; set; }

        public DbSet<SubUser> SubUser { get; set; }

        public DbSet<Spending> Spending { get; set; }

       

    }
}
