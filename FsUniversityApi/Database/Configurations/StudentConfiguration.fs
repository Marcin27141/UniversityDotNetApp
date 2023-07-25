namespace FsUniversityApi.Database.Configurations

open FsUniversityApi.Database.Entities.StudentCourseProfessor
open Microsoft.EntityFrameworkCore
open Microsoft.EntityFrameworkCore.Metadata.Builders
open System

type StudentConfiguration() =
    let initialPersonInfo = PersonConfiguration.initialPersonInfos
    interface IEntityTypeConfiguration<Student> with
        member this.Configure(builder : EntityTypeBuilder<Student>) =
            builder.HasData(
                { PersonInfo = initialPersonInfo.Item(3); PersonInfoId = initialPersonInfo.Item(3).PersonInfoId; Index = "111111"; BeginningOfStudying = DateTime(2022, 10, 1); Courses = ResizeArray<Course>() },
                { PersonInfo = initialPersonInfo.Item(4); PersonInfoId = initialPersonInfo.Item(4).PersonInfoId; Index = "222222"; BeginningOfStudying = DateTime(2021, 10, 2); Courses = ResizeArray<Course>() },
                { PersonInfo = initialPersonInfo.Item(5); PersonInfoId = initialPersonInfo.Item(5).PersonInfoId; Index = "333333"; BeginningOfStudying = DateTime(2021, 10, 2); Courses = ResizeArray<Course>() }
                ) |> ignore