namespace FsUniversityApi.Database

open Microsoft.EntityFrameworkCore
open FsUniversityApi.Database.Entities.Professor
open FsUniversityApi.Database.Entities.Person
open FsUniversityApi.Database.Entities.StudentAndCourse
open FsUniversityApi.Database.Configurations
open EntityFrameworkCore.FSharp.Extensions

module FsDbContext =

    type FsDbContext(options : DbContextOptions<FsDbContext>) =
        inherit DbContext(options)

        [<DefaultValue>]
        val mutable PeopleSet : DbSet<PersonInfo>
        member public this.People   with get()   = this.PeopleSet 
                                    and     set value  = this.PeopleSet <- value

        [<DefaultValue>]
        val mutable CoursesSet : DbSet<Course>
        member public this.Courses  with    get()   = this.CoursesSet 
                                    and     set value  = this.CoursesSet <- value

        //[<DefaultValue>]
        //val mutable StudentCourse : DbSet<CourseEnrollments>
        //member public this.CourseEnrollments  with    get()   = this.StudentCourse 
        //                                        and     set value  = this.StudentCourse <- value

        override this.OnModelCreating(modelBuilder : ModelBuilder) =
            base.OnModelCreating(modelBuilder)

            modelBuilder.Entity<Student>().ToTable("Students") |> ignore
            modelBuilder.Entity<Professor>().ToTable("Professors") |> ignore

            modelBuilder.RegisterOptionTypes()

            modelBuilder.Entity<Professor>().HasOne(fun p -> p.PersonInfo).WithOne() |> ignore
            modelBuilder.Entity<Student>().HasOne(fun s -> s.PersonInfo).WithOne() |> ignore

            //modelBuilder.ApplyConfiguration(PersonConfiguration()) |> ignore
            //modelBuilder.ApplyConfiguration(ProfessorConfiguration()) |> ignore
            //modelBuilder.ApplyConfiguration(CourseConfiguration()) |> ignore
            //modelBuilder.ApplyConfiguration(StudentConfiguration()) |> ignore

    