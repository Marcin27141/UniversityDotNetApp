namespace FsUniversityApi.Database.Entities

open System.ComponentModel.DataAnnotations
open System
open FsUniversityApi.Database.Entities.ISoftRemovableEntity
open ApiDtoLibrary.Person

module Person =
    [<CLIMutable>]
    type PersonInfo =
        {
            [<Key>]
            PersonInfoId : Guid

            [<Required>]
            ApplicationUserId : Guid

            [<Required>]
            FirstName : string

            [<Required>]
            LastName : string

            [<Required>]
            PESEL: string

            Birthday : DateTime
            MotherLand : string
            PersonStatus : PersonStatus
            SoftDeleted : bool
        }
        interface ISoftRemovableEntity with
            member x.SoftDeleted = x.SoftDeleted

    type IPerson = 
        abstract PersonInfo : PersonInfo