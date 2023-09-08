using Microsoft.EntityFrameworkCore;
using UniversityApi.API.DataBase;
using UniversityApi.API.DataBase.Entities;
using UniversityApi.API.GraphQL.Courses;
using UniversityApi.API.GraphQL.Professors;
using UniversityApi.API.GraphQL.Students;

namespace UniversityApi.API.GraphQL
{
    public class Mutation
    {
        public async Task<AddProfessorPayload> AddProfessorAsync(AddProfessorInput input,
            UniversityApiDbContext dbContext)
        {
            var professor = new EntityProfessor
            {
                ApplicationUserId = input.ApplicationUserId,
                FirstName = input.FirstName,
                LastName = input.LastName,
                PersonStatus = ApiDtoLibrary.Person.PersonStatus.Professor,
                IdCode = input.IdCode,
                Subject = input.Subject
            };

            dbContext.Add(professor);
            await dbContext.SaveChangesAsync();

            return new AddProfessorPayload(professor);
        }

        public async Task<AddCoursePayload> AddCourseAsync(AddCourseInput input,
            UniversityApiDbContext dbContext)
        {
            var course = new EntityCourse
            {
                Name = input.Name,
                CourseCode = input.CourseCode,
                ECTS = input.ECTS,
                IsFinishedWithExam = input.IsFinishedWithExam,
                Professor = await dbContext.Set<EntityProfessor>().FindAsync(input.ProfessorId)
            };

            dbContext.Add(course);
            await dbContext.SaveChangesAsync();

            return new AddCoursePayload(course);
        }

        public async Task<AddStudentPayload> AddStudentAsync(AddStudentInput input,
            UniversityApiDbContext dbContext)
        {
            var student = new EntityStudent
            {
                ApplicationUserId = input.ApplicationUserId,
                FirstName = input.FirstName,
                LastName = input.LastName,
                PersonStatus = ApiDtoLibrary.Person.PersonStatus.Student,
                Index = input.Index
            };

            dbContext.Add(student);
            await dbContext.SaveChangesAsync();

            return new AddStudentPayload(student);
        }
    }
}
