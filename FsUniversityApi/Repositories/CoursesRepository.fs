namespace FsUniversityApi.Repositories

open FsUniversityApi.Database.FsDbContext
open FsUniversityApi.Database.Entities.StudentCourseProfessor
open FsUniversityApi.Contracts
open Microsoft.EntityFrameworkCore;
open System
open System.Threading.Tasks

type CoursesRepository(context: FsDbContext) =
    inherit GenericRepository<Course>(context)

    member this.GetAllAsync () =
            context.Courses.Include(fun c -> c.Students).ToListAsync()

    member this.GetAsync (id : Guid) =
            context.Courses.Include(fun c -> c.Students).SingleOrDefaultAsync(fun c -> c.CourseId.Equals(id))

    interface ICoursesRepository with

        member this.CourseCodeIsOccupied (courseCode : string) =
            context.Set<Course>().AnyAsync(fun c -> c.CourseCode.Equals(courseCode));

        member this.UpdateWithProfessorId (course : Course, professorId : Guid) : Task<Course> =
            async {
                let! professor = context.Set<Professor>().FindAsync(professorId).AsTask() |> Async.AwaitTask
                let updated = { course with Professors = ResizeArray([professor])}
                context.Remove(course) |> ignore
                do! context.AddAsync(updated).AsTask() |> Async.AwaitTask |> Async.Ignore
                do! context.SaveChangesAsync() |> Async.AwaitTask |> Async.Ignore
                return course
            }
            |> Async.StartAsTask
            
        member this.AddWithProfessorId (course : Course, professorId : Guid) =
            async {
                let! professor = context.Set<Professor>().FindAsync(professorId).AsTask() |> Async.AwaitTask
                let courseWithProfessor = { course with Professors = ResizeArray([professor]) }
                do! context.AddAsync(courseWithProfessor).AsTask() |> Async.AwaitTask |> Async.Ignore
                do! context.SaveChangesAsync() |> Async.AwaitTask |> Async.Ignore
                return course
            }
            |> Async.StartAsTask
        