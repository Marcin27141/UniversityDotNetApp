using GrpcService.Models;
using Microsoft.EntityFrameworkCore;

namespace GrpcService.Database
{
    public class GrpcDbContext : DbContext
    {
        public GrpcDbContext(DbContextOptions options) : base (options)
        {
                
        }
        public DbSet<Course> Courses => Set<Course>();
        public DbSet<Grade> Grades => Set<Grade>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Person>()
                .HasKey(p => p.PersonId);

            modelBuilder.Entity<Grade>()
                .Property(c => c.GradeValue)
                .HasPrecision(3, 1);

            modelBuilder.Entity<Course>()
                .HasMany(c => c.Grades)
                .WithOne(g => g.GradedCourse)
                .HasForeignKey(g => g.GradedCourseId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Student>()
                .HasMany(s => s.Grades)
                .WithOne(g => g.GradedStudent)
                .HasForeignKey(g => g.GradedStudentId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
