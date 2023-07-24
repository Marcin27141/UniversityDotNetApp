namespace FsUniversityApi.Repositories

open Microsoft.EntityFrameworkCore
open FsUniversityApi.Database.FsDbContext
open FsUniversityApi.Database.Entities.ISoftRemovableEntity
open FsUniversityApi.Contracts
open System.Threading.Tasks
open System

type GenericRepository<'T when 'T :> ISoftRemovableEntity and 'T : not struct>(context : FsDbContext) =
    interface IGenericRepository<'T> with
        member this.AddAsync (entity : 'T) =
            async {
                do! context.AddAsync(entity).AsTask() |> Async.AwaitTask |> Async.Ignore
                do! context.SaveChangesAsync() |> Async.AwaitTask |> Async.Ignore
                return entity
            }
            |> Async.StartAsTask

        member this.DeleteAsync (id : Guid) =
            async {
                let! (entity : ISoftRemovableEntity) = context.FindAsync(id).AsTask() |> Async.AwaitTask
                let updatedEntity = entity.SetSoftDeleted(true)
                context.Remove(entity) |> ignore
                do! context.AddAsync(entity).AsTask() |> Async.AwaitTask |> Async.Ignore
                return! context.SaveChangesAsync() |> Async.AwaitTask
            }
            |> Async.StartAsTask :> Task

        member this.Exists (id : Guid) =
            async {
                let! entity = context.FindAsync(id).AsTask() |> Async.AwaitTask
                return entity <> null
            }
            |> Async.StartAsTask

        member this.GetAllAsync () =
            context.Set<'T>().ToListAsync()

        member this.GetAsync (id : Guid) =
            context.Set<'T>().FindAsync(id).AsTask()

        member this.UpdateAsync (entity : 'T) =
            async {
                let! entity = context.FindAsync(id).AsTask() |> Async.AwaitTask
                context.Remove(entity) |> ignore
                do! context.AddAsync(entity).AsTask() |> Async.AwaitTask |> Async.Ignore
                return! context.SaveChangesAsync() |> Async.AwaitTask
            }
            |> Async.StartAsTask :> Task