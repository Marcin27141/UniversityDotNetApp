using ApiDtoLibrary.Courses;
using ApiDtoLibrary.GraphQL.Courses;
using ApiDtoLibrary.GraphQL.Professors;
using ApiDtoLibrary.GraphQL.Students;
using ApiDtoLibrary.GraphQL.Test;
using ApiDtoLibrary.Person;
using ApiDtoLibrary.Professors;
using ApiDtoLibrary.Students;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UniversityApi.API.DataBase;
using UniversityApi.API.DataBase.Entities;

namespace UniversityApi.API.GraphQL.Mutations
{
    public class Mutation
    {
        public async Task<AddTestPayload> AddTestAsync(AddTestInput input)
        {
            return await Task.FromResult(new AddTestPayload(true));
        }

        public async Task<AddProfessorPayload> AddProfessorAsync(IMapper _mapper, AddProfessorInput input,
            UniversityApiDbContext dbContext)
        {
            var professor = _mapper.Map<EntityProfessor>(input);

            await dbContext.AddAsync(professor);
            await dbContext.SaveChangesAsync();

            var response = _mapper.Map<GetProfessor>(professor);
            //return response;
            
            return new AddProfessorPayload(professor.EntityPersonID.ToString(), professor.ApplicationUserId.ToString(), professor.FirstName, professor.LastName, professor.IdCode, professor.Subject);
        }

        public async Task<AddCoursePayload> AddCourseAsync(IMapper _mapper, AddCourseInput input,
            UniversityApiDbContext dbContext)
        {
            var course = _mapper.Map<EntityCourse>(input);
            course.Professor = await dbContext.Set<EntityProfessor>().FindAsync(input.ProfessorId);

            await dbContext.AddAsync(course);
            await dbContext.SaveChangesAsync();

            var response = _mapper.Map<GetCourse>(course);
            return new AddCoursePayload(response);
        }

        public async Task<AddStudentPayload> AddStudentAsync(IMapper _mapper, AddStudentInput input,
            UniversityApiDbContext dbContext)
        {
            var student = _mapper.Map<EntityStudent>(input);

            await dbContext.AddAsync(student);
            await dbContext.SaveChangesAsync();

            var response = _mapper.Map<GetStudent>(student);
            return new AddStudentPayload(response);
        }
    }
}
