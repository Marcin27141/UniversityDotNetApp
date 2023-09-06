﻿// <auto-generated />
using System;
using GrpcService.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GrpcService.Migrations
{
    [DbContext(typeof(GrpcDbContext))]
    partial class GrpcDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("GrpcService.Models.PersonalData", b =>
                {
                    b.Property<Guid>("PersonId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ApplicationUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Birthday")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Motherland")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PESEL")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PersonStatus")
                        .HasColumnType("int");

                    b.HasKey("PersonId");

                    b.ToTable("People");
                });

            modelBuilder.Entity("GrpcService.Models.Professor", b =>
                {
                    b.Property<string>("IdCode")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("FirstDayAtJob")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("PersonalDataPersonId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Salary")
                        .HasColumnType("int");

                    b.Property<string>("Subject")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdCode");

                    b.HasIndex("PersonalDataPersonId");

                    b.ToTable("Professors");
                });

            modelBuilder.Entity("GrpcService.Models.Professor", b =>
                {
                    b.HasOne("GrpcService.Models.PersonalData", "PersonalData")
                        .WithMany()
                        .HasForeignKey("PersonalDataPersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PersonalData");
                });
#pragma warning restore 612, 618
        }
    }
}
