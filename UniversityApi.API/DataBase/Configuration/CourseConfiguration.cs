using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;
using UniversityApi.API.DataBase.Entities;

namespace UniversityApi.API.DataBase.Configuration
{
    public class CourseConfiguration : IEntityTypeConfiguration<EntityCourse>
    {
        public void Configure(EntityTypeBuilder<EntityCourse> builder)
        {
            builder.HasData(
                new EntityCourse
                {
                    EntityCourseID = Guid.NewGuid(),
                    CourseCode = "C01",
                    Name = "Databases",
                    ECTS = 2,
                    IsFinishedWithExam = false,
                },
                new EntityCourse
                {
                    EntityCourseID = Guid.NewGuid(),
                    CourseCode = "C02",
                    Name = "Algorithms",
                    ECTS = 3,
                    IsFinishedWithExam = true,
                },
                new EntityCourse
                {
                    EntityCourseID = Guid.NewGuid(),
                    CourseCode = "C03",
                    Name = "Computer science",
                    ECTS = 4,
                    IsFinishedWithExam = true,
                }); ;
        }
    }
}
