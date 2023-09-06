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

            modelBuilder.Entity<PersonalData>()
                .HasKey(p => p.PersonId);

            modelBuilder.Entity<Professor>()
                .HasKey(p => p.IdCode);

        }

        public DbSet<PersonalData> People => Set<PersonalData>();
        public DbSet<Professor> Professors => Set<Professor>();
    }
}
