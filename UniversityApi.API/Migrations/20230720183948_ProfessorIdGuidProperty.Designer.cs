﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UniversityApi.API.DataBase;

#nullable disable

namespace UniversityApi.API.Migrations
{
    [DbContext(typeof(UniversityApiDbContext))]
    [Migration("20230720183948_ProfessorIdGuidProperty")]
    partial class ProfessorIdGuidProperty
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

                    b.Property<Guid?>("ProfessorEntityPersonID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("SoftDeleted")
                        .HasColumnType("bit");

                    b.HasKey("EntityCourseID");

                    b.HasIndex("ProfessorEntityPersonID");

                    b.ToTable("Courses");

                    b.HasData(
                        new
                        {
                            EntityCourseID = new Guid("1b9be93a-e979-49a8-b360-63480bceef60"),
                            CourseCode = "C01",
                            ECTS = 2,
                            IsFinishedWithExam = false,
                            Name = "Databases",
                            SoftDeleted = false
                        },
                        new
                        {
                            EntityCourseID = new Guid("854d8120-023f-434b-8c07-7fce68d52ca0"),
                            CourseCode = "C02",
                            ECTS = 3,
                            IsFinishedWithExam = true,
                            Name = "Algorithms",
                            SoftDeleted = false
                        },
                        new
                        {
                            EntityCourseID = new Guid("1ef5b7de-62fb-4b77-8b65-cf6126265d51"),
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

                    b.Property<bool>("SoftDeleted")
                        .HasColumnType("bit");

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
                        .HasForeignKey("ProfessorEntityPersonID");

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
