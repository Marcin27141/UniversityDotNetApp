namespace FsUniversityApi.Database.Entities

open System.ComponentModel.DataAnnotations
open System
open Person

module Professor = 
    [<CLIMutable>]
        type Professor =
            {
                [<Key>]
                PersonInfoId : Guid
                PersonInfo : PersonInfo

                [<Required>]
                IdCode : string

                [<Required>]
                Subject : string

                FirstDayAtJob : DateTime
                Salary : int
            }
            interface IPerson with
                member x.PersonInfo = x.PersonInfo
                