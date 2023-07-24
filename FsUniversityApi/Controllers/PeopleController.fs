namespace FsUniversityApi.Controllers

open Microsoft.AspNetCore.Mvc
open FsUniversityApi.Database.FsDbContext
open FsUniversityApi.Database.Entities.Person
open Microsoft.EntityFrameworkCore


[<ApiController>]
[<Route("[controller]")>]
type PeopleController (context : FsDbContext) =
    inherit ControllerBase()
    let _context = context

    [<HttpGet>]
    member _.Get() =
        _context.Set<PersonInfo>().ToListAsync()