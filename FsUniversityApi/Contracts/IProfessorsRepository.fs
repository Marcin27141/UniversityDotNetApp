namespace FsUniversityApi.Contracts

open FsUniversityApi.Database.Entities.StudentCourseProfessor
open System.Threading.Tasks

type IProfessorsRepository =
    abstract member IdCodeIsOccupied : string -> Task<bool>