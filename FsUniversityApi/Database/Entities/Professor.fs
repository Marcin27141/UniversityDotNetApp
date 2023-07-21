namespace FsUniversityApi.Database.Entities

open System.ComponentModel.DataAnnotations
open System

module Professor = 
    [<CLIMutable>]
        type Professor =
            {
                ProfessorId : Guid

                [<Required>]
                IdCode : string

                [<Required>]
                Subject : string
            }