namespace FsUniversityApi.Repositories

open FsUniversityApi.Database.FsDbContext
open FsUniversityApi.Contracts
open Microsoft.EntityFrameworkCore;
open System
open FsUniversityApi.Database.Entities.Person
open FsUniversityApi.Database.Entities.ISoftRemovableEntity
open System.Threading.Tasks

type PeopleRepository(context: FsDbContext) =

    //public async Task<EntityPerson> GetAsync(Guid id)
    //    {
    //        return await _context.People.FindAsync(id);
    //    }

    //    public async Task DeleteAsync(Guid id)
    //    {
    //        var entity = await GetAsync(id);
    //        //_context.People.Remove(entity);
    //        entity.SoftDeleted = true;
    //        await _context.SaveChangesAsync();
    //    }

    //    public async Task<List<EntityPerson>> GetAllPersonalDataAsync()
    //    {
    //        return await _context.People.ToListAsync();
    //    }

    interface IPeopleRepository with
        member this.GetAsync (personId : Guid) =
            context.Set<PersonInfo>().FindAsync(personId).AsTask();

        member this.GetAllPersonalDataAsync () =
            context.Set<PersonInfo>().ToListAsync();

        member this.DeleteAsync (personId: Guid) =
            async {
                let! (entity : ISoftRemovableEntity) = context.FindAsync(id).AsTask() |> Async.AwaitTask
                let updatedEntity = entity.SetSoftDeleted(true)
                context.Remove(entity) |> ignore
                do! context.AddAsync(updatedEntity).AsTask() |> Async.AwaitTask |> Async.Ignore
                return! context.SaveChangesAsync() |> Async.AwaitTask
            }
            |> Async.StartAsTask :> Task