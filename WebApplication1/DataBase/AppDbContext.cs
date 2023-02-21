using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.DataBase.Entities;

namespace WebApplication1.DataBase
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }
        public DbSet<EntityProfessor> Professors { get; set; }
        public DbSet<EntityStudent> Students { get; set; }
        public DbSet<EntityPersonalData> PersonalData { get; set; }
        public DbSet<EntityCourse> Courses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<StudentCourse>()
                .HasKey(sc => new { sc.StudentID, sc.CourseID});

            modelBuilder.Entity<StudentCourse>()
                .HasOne(sc => sc.Student)
                .WithMany(s => s.Courses)
                .HasForeignKey(sc => sc.StudentID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<StudentCourse>()
                .HasOne(sc => sc.Course)
                .WithMany(c => c.Students)
                .HasForeignKey(sc => sc.CourseID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<EntityStudent>()
                .HasOne(s => s.PersonalData)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<EntityProfessor>()
                .HasOne(p => p.PersonalData)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<EntityCourse>()
                .HasOne(c => c.Professor)
                .WithMany()
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<EntityPersonalData>().HasQueryFilter(pd => !pd.SoftDeleted);
            modelBuilder.Entity<EntityStudent>().HasQueryFilter(st => !st.SoftDeleted);
            modelBuilder.Entity<EntityProfessor>().HasQueryFilter(pr => !pr.SoftDeleted);
            modelBuilder.Entity<EntityCourse>().HasQueryFilter(c => !c.SoftDeleted);
            modelBuilder.Entity<StudentCourse>().HasQueryFilter(sc => !sc.Course.SoftDeleted);
        }
    }
}
