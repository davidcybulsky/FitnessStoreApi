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
            modelBuilder.Entity<User>(builderAction => { });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_databaseSettings.ConnectionString);
        }
    }
}
