namespace FsUniversityApi.Controllers

open Microsoft.AspNetCore.Mvc
open FsUniversityApi.Database.FsDbContext
open FsUniversityApi.Database.Entities.StudentCourseProfessor
open Microsoft.EntityFrameworkCore


[<ApiController>]
[<Route("[controller]")>]
type ProfessorsController (context : FsDbContext) =
    inherit ControllerBase()
    let _context = context

    [<HttpGet>]
    member _.Get() =
        _context.Set<Professor>().Include(fun p -> p.PersonInfo).ToListAsync()