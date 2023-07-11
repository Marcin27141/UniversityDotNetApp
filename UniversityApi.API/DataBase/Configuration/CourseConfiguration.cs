﻿using Microsoft.EntityFrameworkCore;
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
                    CourseCode = "C01",
                    Name = "Databases",
                    ECTS = 2,
                    IsFinishedWithExam = false,
                    ProfessorID = 4
                },
                new EntityCourse
                {
                    CourseCode = "C02",
                    Name = "Algorithms",
                    ECTS = 3,
                    IsFinishedWithExam = true,
                    ProfessorID = 4
                },
                new EntityCourse
                {
                    CourseCode = "C03",
                    Name = "Computer science",
                    ECTS = 4,
                    IsFinishedWithExam = true,
                    ProfessorID = 5
                }); ;
        }
    }
}
