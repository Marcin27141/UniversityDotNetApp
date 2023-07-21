namespace FsUniversityApi
#nowarn "20"
open Microsoft.AspNetCore.Builder
open Microsoft.Extensions.Configuration
open Microsoft.Extensions.DependencyInjection
open Microsoft.Extensions.Hosting
open Microsoft.EntityFrameworkCore
open FsUniversityApi.Database.FsDbContext

module Program =
    let exitCode = 0

    [<EntryPoint>]
    let main args =

        let builder = WebApplication.CreateBuilder(args)

        let connectionString = builder.Configuration.GetConnectionString("FsApiDbConnectionString")
        builder.Services.AddDbContext<FsDbContext>(fun options -> options.UseSqlServer(connectionString) |> ignore) |> ignore

        builder.Services.AddControllers()

        let app = builder.Build()

        app.UseHttpsRedirection()

        app.UseAuthorization()
        app.MapControllers()

        app.Run()

        exitCode
