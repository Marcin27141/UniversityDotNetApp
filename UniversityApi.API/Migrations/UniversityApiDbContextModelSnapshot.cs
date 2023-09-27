﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UniversityApi.API.DataBase;

#nullable disable

namespace UniversityApi.API.Migrations
{
    [DbContext(typeof(UniversityApiDbContext))]
    partial class UniversityApiDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("EntityCourseEntityStudent", b =>
                {
                    b.Property<Guid>("CoursesEntityCourseID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("StudentsEntityPersonID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("CoursesEntityCourseID", "StudentsEntityPersonID");

                    b.HasIndex("StudentsEntityPersonID");

                    b.ToTable("EntityCourseEntityStudent");
                });

            modelBuilder.Entity("UniversityApi.API.DataBase.Entities.EntityCourse", b =>
                {
                    b.Property<Guid>("EntityCourseID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CourseCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ECTS")
                        .HasColumnType("int");

                    b.Property<bool>("IsFinishedWithExam")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("ProfessorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("SoftDeleted")
                        .HasColumnType("bit");

                    b.HasKey("EntityCourseID");

                    b.HasIndex("ProfessorId");

                    b.ToTable("Courses");

                    b.HasData(
                        new
                        {
                            EntityCourseID = new Guid("f06d7645-06d0-4fd2-adf9-fd92b1d8154c"),
                            CourseCode = "C01",
                            ECTS = 2,
                            IsFinishedWithExam = false,
                            Name = "Databases",
                            SoftDeleted = false
                        },
                        new
                        {
                            EntityCourseID = new Guid("ba73d4b0-951d-460b-987f-baec826aca31"),
                            CourseCode = "C02",
                            ECTS = 3,
                            IsFinishedWithExam = true,
                            Name = "Algorithms",
                            SoftDeleted = false
                        },
                        new
                        {
                            EntityCourseID = new Guid("3e64612e-7dfc-4c98-a781-f44bf31c2fa0"),
                            CourseCode = "C03",
                            ECTS = 4,
                            IsFinishedWithExam = true,
                            Name = "Computer science",
                            SoftDeleted = false
                        });
                });

            modelBuilder.Entity("UniversityApi.API.DataBase.Entities.EntityPerson", b =>
                {
                    b.Property<Guid>("EntityPersonID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ApplicationUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Birthday")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Motherland")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PESEL")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PersonStatus")
                        .HasColumnType("int");

                    b.HasKey("EntityPersonID");

                    b.ToTable("People");

                    b.UseTptMappingStrategy();
                });

            modelBuilder.Entity("UniversityApi.API.DataBase.Entities.EntityProfessor", b =>
                {
                    b.HasBaseType("UniversityApi.API.DataBase.Entities.EntityPerson");

                    b.Property<DateTime>("FirstDayAtJob")
                        .HasColumnType("datetime2");

                    b.Property<string>("IdCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Salary")
                        .HasColumnType("int");

                    b.Property<string>("Subject")
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("Professors", (string)null);
                });

            modelBuilder.Entity("UniversityApi.API.DataBase.Entities.EntityStudent", b =>
                {
                    b.HasBaseType("UniversityApi.API.DataBase.Entities.EntityPerson");

                    b.Property<DateTime>("BeginningOfStudying")
                        .HasColumnType("datetime2");

                    b.Property<string>("Index")
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("Students", (string)null);
                });

            modelBuilder.Entity("EntityCourseEntityStudent", b =>
                {
                    b.HasOne("UniversityApi.API.DataBase.Entities.EntityCourse", null)
                        .WithMany()
                        .HasForeignKey("CoursesEntityCourseID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UniversityApi.API.DataBase.Entities.EntityStudent", null)
                        .WithMany()
                        .HasForeignKey("StudentsEntityPersonID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("UniversityApi.API.DataBase.Entities.EntityCourse", b =>
                {
                    b.HasOne("UniversityApi.API.DataBase.Entities.EntityProfessor", "Professor")
                        .WithMany()
                        .HasForeignKey("ProfessorId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Professor");
                });

            modelBuilder.Entity("UniversityApi.API.DataBase.Entities.EntityProfessor", b =>
                {
                    b.HasOne("UniversityApi.API.DataBase.Entities.EntityPerson", null)
                        .WithOne()
                        .HasForeignKey("UniversityApi.API.DataBase.Entities.EntityProfessor", "EntityPersonID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("UniversityApi.API.DataBase.Entities.EntityStudent", b =>
                {
                    b.HasOne("UniversityApi.API.DataBase.Entities.EntityPerson", null)
                        .WithOne()
                        .HasForeignKey("UniversityApi.API.DataBase.Entities.EntityStudent", "EntityPersonID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
