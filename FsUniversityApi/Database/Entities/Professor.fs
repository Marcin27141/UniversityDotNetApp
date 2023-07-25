namespace FsUniversityApi.Database.Entities

//open System.ComponentModel.DataAnnotations
//open System
//open Person
//open FsUniversityApi.Database.Entities.ISoftRemovableEntity

//module Professor = 
//    [<CLIMutable>]
//        type Professor =
//            {
//                [<Key>]
//                PersonInfoId : Guid
//                PersonInfo : PersonInfo

//                [<Required>]
//                IdCode : string

//                [<Required>]
//                Subject : string

//                FirstDayAtJob : DateTime
//                Salary : int

//                Courses: Courses
//            }
//            interface IPerson with
//                member x.PersonInfo = x.PersonInfo
//            interface ISoftRemovableEntity with
//                member x.SoftDeleted = x.PersonInfo.SoftDeleted
//                member x.SetSoftDeleted flag = { x with PersonInfo = { x.PersonInfo with SoftDeleted = flag } }
                