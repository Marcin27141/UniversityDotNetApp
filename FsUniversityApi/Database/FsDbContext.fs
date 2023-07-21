namespace FsUniversityApi.Database

open Microsoft.EntityFrameworkCore
open FsUniversityApi.Database.Entities.Professor
open FsUniversityApi.Database.Configurations
open System.Linq
open System

module FsDbContext =

    type FsDbContext(options : DbContextOptions<FsDbContext>) =
        inherit DbContext(options)

        [<DefaultValue>]
        val mutable Professors : DbSet<Professor>
        member public this._Professors  with    get()   = this.Professors 
                                        and     set value  = this.Professors <- value

        override this.OnModelCreating(modelBuilder : ModelBuilder) =
            base.OnModelCreating(modelBuilder)
            modelBuilder.ApplyConfiguration(ProfessorConfiguration()) |> ignore

    