﻿using Microsoft.EntityFrameworkCore;
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
                    EntityPersonID = 1,
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
                    EntityPersonID = 2,
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
                    EntityPersonID = 3,
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