namespace FsUniversityApi.Database.Entities

open System.ComponentModel.DataAnnotations
open System
open ISoftRemovableEntity
open System.ComponentModel.DataAnnotations.Schema
open Person

module StudentCourseProfessor =
    [<CLIMutable>]
    type Course =
        {
            [<Key>]
            CourseId : Guid

            [<Required>]
            CourseCode : string

            [<Required>]
            CourseName : string

            ECTS : int
            IsFinishedWithExam : bool
            SoftDeleted : bool

            Students: Student ResizeArray
            Professors: Professor ResizeArray
        }
        interface ISoftRemovableEntity with
            member x.SoftDeleted = x.SoftDeleted
            member x.SetSoftDeleted flag = { x with SoftDeleted = flag }
    and [<CLIMutable>] Student =
        {
            [<Key>]
            PersonInfoId : Guid
            PersonInfo : PersonInfo

            [<Required>]
            Index : string

            BeginningOfStudying : DateTime

            Courses : Course ResizeArray
        }
        interface IPerson with
            member x.PersonInfo = x.PersonInfo
        interface ISoftRemovableEntity with
            member x.SoftDeleted = x.PersonInfo.SoftDeleted
            member x.SetSoftDeleted flag = { x with PersonInfo = { x.PersonInfo with SoftDeleted = flag } }
    and [<CLIMutable>] Professor =
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

                Courses: Course ResizeArray
            }
            interface IPerson with
                member x.PersonInfo = x.PersonInfo
            interface ISoftRemovableEntity with
                member x.SoftDeleted = x.PersonInfo.SoftDeleted
                member x.SetSoftDeleted flag = { x with PersonInfo = { x.PersonInfo with SoftDeleted = flag } }