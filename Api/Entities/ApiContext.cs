using Api.Settings;
using Microsoft.EntityFrameworkCore;

namespace Api.Entities
{
    public class ApiContext : DbContext
    {
        private DatabaseSettings _databaseSettings;

        public DbSet<User> Users { get; set; }

        public ApiContext(DatabaseSettings databaseSettings)
        {
            _databaseSettings = databaseSettings;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(builderAction =>
            {
                builderAction.HasMany(u => u.Subscriptions)
                .WithOne(s => s.User)
                .HasForeignKey(u => u.UserId);
            });

            modelBuilder.Entity<Plan>(builderAction =>
            {
                builderAction.HasData(
                    new Plan
                    {
                        Id = 1,
                        Name = "Standard",
                        Description = "The standard plan description",
                        Price = 29
                    },
                    new Plan
                    {
                        Id = 2,
                        Name = "Plus",
                        Description = "The plus plan description",
                        Price = 49
                    },
                    new Plan
                    {
                        Id = 3,
                        Name = "Premium",
                        Description = "The premium plan description",
                        Price = 99
                    }
                    );
            });

            modelBuilder.Entity<Subscription>(builderAction =>
            {
                builderAction.HasOne(s => s.Plan)
                .WithMany()
                .HasForeignKey(s => s.PlanId);
            });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_databaseSettings.ConnectionString);
        }
    }
}
