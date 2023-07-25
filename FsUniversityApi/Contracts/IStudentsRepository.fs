namespace FsUniversityApi.Contracts

open FsUniversityApi.Database.Entities.StudentCourseProfessor
open System.Threading.Tasks
open System
open System.Collections.Generic

type IStudentsRepository =
    abstract member UpdateWithCoursesAsync : Student * IEnumerable<Guid> -> Task<Guid>
    abstract member IndexIsOccupied : string -> Task<bool>
    abstract member AddWithCoursesAsync : Student * IEnumerable<Guid> -> Task<Student>
    abstract member DeleteStudentsCourseAsync : Guid * Guid -> Task