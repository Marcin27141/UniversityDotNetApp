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
                    b.Property<int>("CoursesEntityCourseID")
                        .HasColumnType("int");

                    b.Property<int>("StudentsEntityPersonID")
                        .HasColumnType("int");

                    b.HasKey("CoursesEntityCourseID", "StudentsEntityPersonID");

                    b.HasIndex("StudentsEntityPersonID");

                    b.ToTable("EntityCourseEntityStudent");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "46c83bc6-1426-49d9-be8f-6083002fa382",
                            ConcurrencyStamp = "1c0af613-1848-49f7-885d-38bf4ea8fbb7",
                            Name = "Administrator",
                            NormalizedName = "ADMINISTRATOR"
                        },
                        new
                        {
                            Id = "32ff77e4-efd4-4e86-a413-437b11057187",
                            ConcurrencyStamp = "027dfbf7-1686-43b7-a6bb-e65fa0ee8641",
                            Name = "User",
                            NormalizedName = "USER"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
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

                    b.HasData(
                        new
                        {
                            EntityCourseID = 1,
                            CourseCode = "C01",
                            ECTS = 2,
                            IsFinishedWithExam = false,
                            Name = "Databases",
                            ProfessorID = 4,
                            SoftDeleted = false
                        },
                        new
                        {
                            EntityCourseID = 2,
                            CourseCode = "C02",
                            ECTS = 3,
                            IsFinishedWithExam = true,
                            Name = "Algorithms",
                            ProfessorID = 4,
                            SoftDeleted = false
                        },
                        new
                        {
                            EntityCourseID = 3,
                            CourseCode = "C03",
                            ECTS = 4,
                            IsFinishedWithExam = true,
                            Name = "Computer science",
                            ProfessorID = 5,
                            SoftDeleted = false
                        });
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

            modelBuilder.Entity("UniversityApi.API.DataBase.Identity.ApiUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
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

                    b.HasData(
                        new
                        {
                            EntityPersonID = 4,
                            Birthday = new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FirstName = "Danuta",
                            LastName = "Dobrzycka",
                            Motherland = "Poland",
                            PESEL = "14444444444",
                            SoftDeleted = false,
                            FirstDayAtJob = new DateTime(2019, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IdCode = "11111",
                            Salary = 10000,
                            Subject = "Programming"
                        },
                        new
                        {
                            EntityPersonID = 5,
                            Birthday = new DateTime(1999, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FirstName = "Eustachy",
                            LastName = "Epoletnik",
                            Motherland = "Poland",
                            PESEL = "15555555555",
                            SoftDeleted = false,
                            FirstDayAtJob = new DateTime(2018, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IdCode = "22222",
                            Salary = 9000,
                            Subject = "Science"
                        },
                        new
                        {
                            EntityPersonID = 6,
                            Birthday = new DateTime(1998, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FirstName = "Filomena",
                            LastName = "Fomicz",
                            Motherland = "Poland",
                            PESEL = "16666666666",
                            SoftDeleted = false,
                            FirstDayAtJob = new DateTime(2017, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IdCode = "33333",
                            Salary = 8000,
                            Subject = "Philosophy"
                        });
                });

            modelBuilder.Entity("UniversityApi.API.DataBase.Entities.EntityStudent", b =>
                {
                    b.HasBaseType("UniversityApi.API.DataBase.Entities.EntityPerson");

                    b.Property<DateTime>("BeginningOfStudying")
                        .HasColumnType("datetime2");

                    b.Property<string>("Index")
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("Students", (string)null);

                    b.HasData(
                        new
                        {
                            EntityPersonID = 1,
                            Birthday = new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FirstName = "Adam",
                            LastName = "Adamczyk",
                            Motherland = "Poland",
                            PESEL = "01111111111",
                            SoftDeleted = false,
                            BeginningOfStudying = new DateTime(2019, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Index = "111111"
                        },
                        new
                        {
                            EntityPersonID = 2,
                            Birthday = new DateTime(1999, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FirstName = "Bartosz",
                            LastName = "Bednarek",
                            Motherland = "Poland",
                            PESEL = "0222222222",
                            SoftDeleted = false,
                            BeginningOfStudying = new DateTime(2018, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Index = "222222"
                        },
                        new
                        {
                            EntityPersonID = 3,
                            Birthday = new DateTime(1998, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FirstName = "Celina",
                            LastName = "Czarna",
                            Motherland = "Poland",
                            PESEL = "03333333333",
                            SoftDeleted = false,
                            BeginningOfStudying = new DateTime(2017, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Index = "333333"
                        });
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

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("UniversityApi.API.DataBase.Identity.ApiUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("UniversityApi.API.DataBase.Identity.ApiUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UniversityApi.API.DataBase.Identity.ApiUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("UniversityApi.API.DataBase.Identity.ApiUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
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
