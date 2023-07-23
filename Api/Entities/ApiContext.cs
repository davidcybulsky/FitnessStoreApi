using Microsoft.EntityFrameworkCore;

namespace Api.Entities
{
    public class ApiContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(builderAction => { });
        }
    }
}
