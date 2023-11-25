using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;
using UniversityApi.API.DataBase.Entities;

namespace UniversityApi.API.DataBase.Configuration
{
    public class ProfessorConfiguration : IEntityTypeConfiguration<EntityProfessor>
    {
        public void Configure(EntityTypeBuilder<EntityProfessor> builder)
        {
            builder.HasData(
                new EntityProfessor
                {
                    EntityPersonID = Guid.Parse("18b517f2-b789-4230-e63c-08dbbf4015ce"),
                    FirstName = "Danuta",
                    LastName = "Dobrzycka",
                    PESEL = "14444444444",
                    Birthday = new DateTime(2000, 1, 1),
                    Motherland = "Poland",
                    IdCode = "11111",
                    Subject = "Programming",
                    FirstDayAtJob = new DateTime(2019, 10, 1),
                    Salary = 10000
                },
                new EntityProfessor
                {
                    EntityPersonID = Guid.Parse("28b517f2-b789-4230-e63c-08dbbf4015ce"),
                    FirstName = "Eustachy",
                    LastName = "Epoletnik",
                    PESEL = "15555555555",
                    Birthday = new DateTime(1999, 2, 2),
                    Motherland = "Poland",
                    IdCode = "22222",
                    Subject = "Science",
                    FirstDayAtJob = new DateTime(2018, 10, 1),
                    Salary = 9000
                });
        }
    }
}
