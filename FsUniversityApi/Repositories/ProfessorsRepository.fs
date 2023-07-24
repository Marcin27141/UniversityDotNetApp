namespace FsUniversityApi.Repositories

open FsUniversityApi.Database.FsDbContext
open FsUniversityApi.Database.Entities.Professor
open FsUniversityApi.Contracts
open Microsoft.EntityFrameworkCore;

type ProfessorsRepository(context: FsDbContext) =
    inherit GenericRepository<Professor>(context)

    interface IProfessorsRepository with
        member this.IdCodeIsOccupied (idCode : string) =
            context.Set<Professor>().AnyAsync(fun p -> p.IdCode.Equals(idCode));