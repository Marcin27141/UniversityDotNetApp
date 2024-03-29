﻿using Microsoft.EntityFrameworkCore;
using UniversityApi.API.DataBase.Configuration;
using UniversityApi.API.DataBase.Entities;

namespace UniversityApi.API.DataBase
{
    public class UniversityApiDbContext : DbContext
    {
        public UniversityApiDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<EntityPerson> People { get; set; }
        public DbSet<EntityCourse> Courses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<EntityStudent>()
                .ToTable("Students");
            modelBuilder.Entity<EntityProfessor>()
                .ToTable("Professors");

            modelBuilder.Entity<EntityCourse>()
                .HasOne(c => c.Professor)
                .WithMany()
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<EntityPerson>()
                .HasMany(p => p.Notifications)
                .WithOne(n => n.Recipient)
                .HasForeignKey(p => p.RecipientId)
                .OnDelete(DeleteBehavior.Cascade);


            //modelBuilder.ApplyConfiguration(new StudentConfiguration());
            //modelBuilder.ApplyConfiguration(new ProfessorConfiguration());
            //modelBuilder.ApplyConfiguration(new CourseConfiguration());
        }
    }
}
