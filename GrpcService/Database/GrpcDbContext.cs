using GrpcService.Models;
using Microsoft.EntityFrameworkCore;

namespace GrpcService.Database
{
    public class GrpcDbContext : DbContext
    {
        public GrpcDbContext(DbContextOptions options) : base (options)
        {
                
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Person>().HasQueryFilter(p => !p.SoftDeleted);

        }

        public DbSet<Person> People => Set<Person>();
    }
}
