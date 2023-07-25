namespace FsUniversityApi.Controllers

open Microsoft.AspNetCore.Mvc
open FsUniversityApi.Database.FsDbContext
open FsUniversityApi.Database.Entities.StudentCourseProfessor
open Microsoft.EntityFrameworkCore


[<ApiController>]
[<Route("[controller]")>]
type StudentsController (context : FsDbContext) =
    inherit ControllerBase()
    let _context = context

    [<HttpGet>]
    member _.Get() =
        _context.Set<Student>().Include(fun p -> p.PersonInfo).ToListAsync()

