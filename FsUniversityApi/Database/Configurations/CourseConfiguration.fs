namespace FsUniversityApi.Database.Configurations

//open FsUniversityApi.Database.Entities.Course
//open Microsoft.EntityFrameworkCore
//open Microsoft.EntityFrameworkCore.Metadata.Builders
//open System

//type CourseConfiguration() =
//    interface IEntityTypeConfiguration<Course> with
//        member this.Configure(builder : EntityTypeBuilder<Course>) =
//            builder.HasData(
//                //{ CourseId = Guid.NewGuid(); CourseCode = "C01"; CourseName = "Java course"; IsFinishedWithExam = false; ECTS = 2; SoftDeleted = false },
//                //{ CourseId = Guid.NewGuid(); CourseCode = "C02"; CourseName = "Databases"; IsFinishedWithExam = true; ECTS = 4; SoftDeleted = false },
//                //{ CourseId = Guid.NewGuid(); CourseCode = "C03"; CourseName = "Algorithms"; IsFinishedWithExam = true; ECTS = 5; SoftDeleted = false }
//                ) |> ignore