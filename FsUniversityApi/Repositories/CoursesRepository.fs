namespace FsUniversityApi.Repositories

open FsUniversityApi.Database.FsDbContext
open FsUniversityApi.Database.Entities.StudentAndCourse
open FsUniversityApi.Database.Entities.Professor
open FsUniversityApi.Contracts
open Microsoft.EntityFrameworkCore;
open System

type CoursesRepository(context: FsDbContext) =
    inherit GenericRepository<Course>(context)

    interface ICoursesRepository with
        member this.CourseCodeIsOccupied (courseCode : string) =
            context.Set<Course>().AnyAsync(fun c -> c.CourseCode.Equals(courseCode));

        member this.UpdateWithProfessorId (course : Course, professorId : Guid) =
        //course.Professor = _context.Set<EntityProfessor>().Find(professorId);
        //    _context.Update(course);
        //    await _context.SaveChangesAsync();
        //    return course;
            let! professor = context.Set<Professor>().FindAsync(professorId) |> Async.AwaitTask
            let updated = { course with Professor = professor }
            context.Remove(entity) |> ignore
            do! context.AddAsync(entity).AsTask() |> Async.AwaitTask |> Async.Ignore
            return! context.SaveChangesAsync() |> Async.AwaitTask

        //member this.AddWithProfessorId (course : Course, professorId : Guid) =
        //    context.Set<Course>().AnyAsync(fun c -> c.CourseCode.Equals(courseCode));

        //public override async Task<EntityCourse> GetAsync(Guid id)
        //{
        //    return await _context.Courses
        //        .Include(c => c.Professor)
        //        .Include(c => c.Students)
        //        .SingleOrDefaultAsync(c => c.EntityCourseID == id);
        //}

        //public override async Task<EntityCourse> AddAsync(EntityCourse entity)
        //{
        //    await _context.AddAsync(entity);
        //    await _context.SaveChangesAsync();
        //    return entity;
        //}

        //public override async Task<List<EntityCourse>> GetAllAsync()
        //{
        //    return await _context.Courses
        //        .Include(c => c.Professor)
        //        .Include(c => c.Students)
        //        .ToListAsync();
        //}

        //public async Task<bool> CourseCodeIsOccupied(string courseCode)
        //{
        //    return await _context.Set<EntityCourse>().AnyAsync(s => s.CourseCode.Equals(courseCode));
        //}

        //public async Task<EntityCourse> AddWithProfessorId(EntityCourse entity, Guid professorId)
        //{
        //    entity.Professor = await _context.Set<EntityProfessor>().FindAsync(professorId);
        //    await _context.AddAsync(entity);
        //    await _context.SaveChangesAsync();
        //    return entity;
        //}

        //public async Task<EntityCourse> UpdateWithProfessorId(EntityCourse course, Guid professorId)
        //{
        //    course.Professor = _context.Set<EntityProfessor>().Find(professorId);
        //    _context.Update(course);
        //    await _context.SaveChangesAsync();
        //    return course;
        //}(*
        