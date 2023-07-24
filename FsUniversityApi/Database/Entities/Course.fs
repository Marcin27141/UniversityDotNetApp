namespace FsUniversityApi.Database.Entities

open System.ComponentModel.DataAnnotations
open System
open FsUniversityApi.Database.Entities.Professor
open ISoftRemovableEntity
open System.ComponentModel.DataAnnotations.Schema

//module Course =
//    [<CLIMutable>]
//    type Course =
//        {
//            [<Key>]
//            CourseId : Guid

//            [<Required>]
//            CourseCode : string

//            [<Required>]
//            CourseName : string

//            ECTS : int
//            IsFinishedWithExam : bool
//            SoftDeleted : bool
//        }
//        interface ISoftRemovableEntity with
//            member x.SoftDeleted = x.SoftDeleted