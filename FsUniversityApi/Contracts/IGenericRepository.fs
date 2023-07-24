namespace FsUniversityApi.Contracts

open System
open System.Collections.Generic
open FsUniversityApi.Database.Entities.ISoftRemovableEntity
open System.Threading.Tasks

type IGenericRepository<'T when 'T :> ISoftRemovableEntity> =
    abstract member AddAsync : 'T -> Task<'T>
    abstract member DeleteAsync : Guid -> Task
    abstract member Exists : Guid -> Task<bool>
    abstract member GetAllAsync : unit -> Task<List<'T>>
    abstract member GetAsync : Guid -> Task<'T>
    abstract member UpdateAsync : 'T -> Task