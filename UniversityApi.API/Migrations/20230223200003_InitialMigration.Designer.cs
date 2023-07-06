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
    [Migration("20230223200003_InitialMigration")]
    partial class InitialMigration
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
                    b.Property<int>("CoursesEntityCourseID")
                        .HasColumnType("int");

                    b.Property<int>("StudentsEntityPersonID")
                        .HasColumnType("int");

                    b.HasKey("CoursesEntityCourseID", "StudentsEntityPersonID");

                    b.HasIndex("StudentsEntityPersonID");

                    b.ToTable("EntityCourseEntityStudent");
                });

            modelBuilder.Entity("UniversityApi.API.DataBase.Entities.EntityCourse", b =>
                {
                    b.Property<int>("EntityCourseID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EntityCourseID"));

                    b.Property<string>("CourseCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ECTS")
                        .HasColumnType("int");

                    b.Property<bool>("IsFinishedWithExam")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ProfessorID")
                        .HasColumnType("int");

                    b.Property<bool>("SoftDeleted")
                        .HasColumnType("bit");

                    b.HasKey("EntityCourseID");

                    b.HasIndex("ProfessorID");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("UniversityApi.API.DataBase.Entities.EntityPerson", b =>
                {
                    b.Property<int>("EntityPersonID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EntityPersonID"));

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
                        .HasForeignKey("ProfessorID");

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
