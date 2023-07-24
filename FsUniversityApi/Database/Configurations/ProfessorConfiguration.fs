namespace FsUniversityApi.Database.Configurations

open FsUniversityApi.Database.Entities.Professor
open Microsoft.EntityFrameworkCore
open Microsoft.EntityFrameworkCore.Metadata.Builders
open System
open FsUniversityApi.Database.Configurations

type ProfessorConfiguration() =
    let initialPersonInfo = PersonConfiguration.initialPersonInfos
    interface IEntityTypeConfiguration<Professor> with
        member this.Configure(builder : EntityTypeBuilder<Professor>) =
            builder.HasData(
                { PersonInfo = initialPersonInfo.Item(0); PersonInfoId = initialPersonInfo.Item(0).PersonInfoId; IdCode = "11111"; Subject = "Programming"; Salary = 5000; FirstDayAtJob = DateTime(2018, 10, 1) },
                { PersonInfo = initialPersonInfo.Item(1); PersonInfoId = initialPersonInfo.Item(1).PersonInfoId; IdCode = "22222"; Subject = "Mathematics"; Salary = 6000; FirstDayAtJob = DateTime(2020, 10, 5)  },
                { PersonInfo = initialPersonInfo.Item(2); PersonInfoId = initialPersonInfo.Item(2).PersonInfoId; IdCode = "33333"; Subject = "Physics"; Salary = 4500; FirstDayAtJob = DateTime(2005, 3, 10)  }
                ) |> ignore