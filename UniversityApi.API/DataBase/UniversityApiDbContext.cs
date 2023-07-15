using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UniversityApi.API.DataBase.Configuration;
using UniversityApi.API.DataBase.Entities;
using UniversityApi.API.DataBase.Identity;

namespace UniversityApi.API.DataBase
{
    public class UniversityApiDbContext : IdentityDbContext<ApiUser>
    {
        public UniversityApiDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<EntityPerson> People { get; set; }
        public DbSet<EntityCourse> Courses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<EntityStudent>()
                .ToTable("Students");
            modelBuilder.Entity<EntityProfessor>()
                .ToTable("Professors");

            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            //modelBuilder.ApplyConfiguration(new StudentConfiguration());
            //modelBuilder.ApplyConfiguration(new ProfessorConfiguration());
            modelBuilder.ApplyConfiguration(new CourseConfiguration());
        }
    }
}
