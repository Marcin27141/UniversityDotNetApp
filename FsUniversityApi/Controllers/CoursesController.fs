namespace FsUniversityApi.Controllers

open Microsoft.AspNetCore.Mvc
open FsUniversityApi.Database.FsDbContext
open FsUniversityApi.Database.Entities.StudentCourseProfessor
open Microsoft.EntityFrameworkCore


[<ApiController>]
[<Route("[controller]")>]
type CoursesController (context : FsDbContext) =
    inherit ControllerBase()
    let _context = context

    [<HttpGet>]
    member _.Get() =
        _context.Set<Course>().Include(fun c -> c.Students).ToListAsync()