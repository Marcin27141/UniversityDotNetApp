namespace FsUniversityApi.Database.Configurations

open FsUniversityApi.Database.Entities.Professor
open Microsoft.EntityFrameworkCore
open Microsoft.EntityFrameworkCore.Metadata.Builders
open System

type ProfessorConfiguration() =
    interface IEntityTypeConfiguration<Professor> with
        member this.Configure(builder : EntityTypeBuilder<Professor>) =
            builder.HasData(
                { ProfessorId = Guid.NewGuid(); IdCode = "11111"; Subject = "Programming" },
                { ProfessorId = Guid.NewGuid(); IdCode = "22222"; Subject = "Mathematics" },
                { ProfessorId = Guid.NewGuid(); IdCode = "33333"; Subject = "Physics" }
                ) |> ignore