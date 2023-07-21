namespace FsUniversityApi.Controllers

open System
open System.Collections.Generic
open System.Linq
open System.Threading.Tasks
open Microsoft.AspNetCore.Mvc
open Microsoft.Extensions.Logging
open FsUniversityApi
open FsUniversityApi.Database.FsDbContext

[<ApiController>]
[<Route("[controller]")>]
type ProfessorsController (context : FsDbContext) =
    inherit ControllerBase()
    let _context = context

    [<HttpGet>]
    member _.Get() =
        _context.Professors