namespace FsUniversityApi.Contracts

open FsUniversityApi.Database.Entities.Professor
open System.Threading.Tasks

type IProfessorsRepository =
    abstract member IdCodeIsOccupied : string -> Task<bool>