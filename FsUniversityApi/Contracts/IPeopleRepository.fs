namespace FsUniversityApi.Contracts

open FsUniversityApi.Database.Entities.Person
open System.Threading.Tasks
open System
open System.Collections.Generic

type IPeopleRepository =
    abstract member GetAsync : Guid -> Task<PersonInfo>
    abstract member GetAllPersonalDataAsync : unit -> Task<List<PersonInfo>>
    abstract member DeleteAsync : Guid -> Task