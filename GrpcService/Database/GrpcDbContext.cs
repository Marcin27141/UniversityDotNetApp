using GrpcService.Models;
using Microsoft.EntityFrameworkCore;

namespace GrpcService.Database
{
    public class GrpcDbContext : DbContext
    {
        public GrpcDbContext(DbContextOptions options) : base (options)
        {
                
        }

        public DbSet<Person> People => Set<Person>();
    }
}
