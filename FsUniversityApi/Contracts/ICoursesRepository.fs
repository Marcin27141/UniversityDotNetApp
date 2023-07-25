namespace FsUniversityApi.Contracts

open FsUniversityApi.Database.Entities.StudentCourseProfessor
open System.Threading.Tasks
open System

type ICoursesRepository =
    abstract member UpdateWithProfessorId : Course * Guid -> Task<Course>
    abstract member CourseCodeIsOccupied : string -> Task<bool>
    abstract member AddWithProfessorId : Course * Guid -> Task<Course>