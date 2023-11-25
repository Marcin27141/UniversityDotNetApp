using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;
using UniversityApi.API.DataBase.Entities;

namespace UniversityApi.API.DataBase.Configuration
{
    public class StudentConfiguration : IEntityTypeConfiguration<EntityStudent>
    {
        public void Configure(EntityTypeBuilder<EntityStudent> builder)
        {
            builder.HasData(
                new EntityStudent
                {
                    EntityPersonID = Guid.Parse("5f20e76f-8f68-422d-e63d-08dbbf4015ce"),
                    FirstName = "Adam",
                    LastName = "Adamczyk",
                    PESEL = "01111111111",
                    Birthday = new DateTime(2000, 1, 1),
                    Motherland = "Poland",
                    Index = "111111",
                    BeginningOfStudying = new DateTime(2019, 10, 1),
                },
                new EntityStudent
                {
                    EntityPersonID = Guid.Parse("1f20e76f-8f68-422d-e63d-08dbbf4015ce"),
                    FirstName = "Bartosz",
                    LastName = "Bednarek",
                    PESEL = "0222222222",
                    Birthday = new DateTime(1999, 2, 2),
                    Motherland = "Poland",
                    Index = "222222",
                    BeginningOfStudying = new DateTime(2018, 10, 1),
                },
                new EntityStudent
                {
                    EntityPersonID = Guid.Parse("2f20e76f-8f68-422d-e63d-08dbbf4015ce"),
                    FirstName = "Celina",
                    LastName = "Czarna",
                    PESEL = "03333333333",
                    Birthday = new DateTime(1998, 3, 3),
                    Motherland = "Poland",
                    Index = "333333",
                    BeginningOfStudying = new DateTime(2017, 10, 1),
                });
        }
    }
}
