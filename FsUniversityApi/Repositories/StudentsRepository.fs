namespace FsUniversityApi.Repositories

open FsUniversityApi.Database.FsDbContext
open FsUniversityApi.Database.Entities.StudentCourseProfessor
open FsUniversityApi.Contracts
open Microsoft.EntityFrameworkCore;
open System
open System.Threading.Tasks
open System.Collections.Generic
open System.Linq

type StudentsRepository(context: FsDbContext) =
    inherit GenericRepository<Student>(context)

    member this.GetAllAsync () =
            context.Set<Student>().Include(fun s -> s.Courses).ToListAsync()

    member this.GetAsync (id : Guid) =
            context.Set<Student>().Include(fun s -> s.Courses).SingleOrDefaultAsync(fun s -> s.PersonInfoId.Equals(id))

    interface IStudentsRepository with

        member this.IndexIsOccupied (index : string) =
            context.Set<Student>().AnyAsync(fun s -> s.Index.Equals(index));

        member this.UpdateWithCoursesAsync (student : Student, coursesIds: IEnumerable<Guid>) : Task<Guid> =
            async {
                let! courses = coursesIds.Select(fun id -> context.Courses.FindAsync(id).AsTask() |> Async.AwaitTask) |> Async.Parallel
                let updated = { student with Courses = ResizeArray(courses) }
                context.Remove(student) |> ignore
                do! context.AddAsync(updated).AsTask() |> Async.AwaitTask |> Async.Ignore
                do! context.SaveChangesAsync() |> Async.AwaitTask |> Async.Ignore
                return student.PersonInfoId
            }
            |> Async.StartAsTask
            
        member this.AddWithCoursesAsync (student : Student, coursesIds: IEnumerable<Guid>) =
            async {
                let! courses = coursesIds.Select(fun id -> context.Courses.FindAsync(id).AsTask() |> Async.AwaitTask) |> Async.Parallel
                let created = { student with Courses = ResizeArray(courses) }
                do! context.AddAsync(created).AsTask() |> Async.AwaitTask |> Async.Ignore
                do! context.SaveChangesAsync() |> Async.AwaitTask |> Async.Ignore
                return student
            }
            |> Async.StartAsTask

        member this.DeleteStudentsCourseAsync (studentId: Guid, courseId: Guid) =
            async {
                let! student = context.Set<Student>().FindAsync(studentId).AsTask() |> Async.AwaitTask
                let! courseToDelete = context.Courses.FindAsync(courseId).AsTask() |> Async.AwaitTask
                let updatedCourses = student.Courses |> List.ofSeq |> List.filter (fun course -> course.CourseId <> courseToDelete.CourseId)
                let updated = { student with Courses = ResizeArray(updatedCourses) }
                context.Remove(student) |> ignore
                do! context.AddAsync(updated).AsTask() |> Async.AwaitTask |> Async.Ignore
                return! context.SaveChangesAsync() |> Async.AwaitTask
            }
            |> Async.StartAsTask :> Task
        