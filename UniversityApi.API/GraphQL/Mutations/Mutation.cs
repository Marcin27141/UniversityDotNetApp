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
    public partial class Mutation
    {
        private readonly IMapper _mapper;

        public Mutation(IMapper mapper)
        {
            this._mapper = mapper;
        }

        public async Task<AddTestPayload> AddTestAsync(AddTestInput input)
        {
            return await Task.FromResult(new AddTestPayload(true));
        }

        public async Task<AddCoursePayload> AddCourseAsync(AddCourseInput input,
            UniversityApiDbContext dbContext)
        {
            var course = _mapper.Map<EntityCourse>(input);
            course.Professor = await dbContext.Set<EntityProfessor>().FindAsync(input.ProfessorId);

            await dbContext.AddAsync(course);
            await dbContext.SaveChangesAsync();

            var response = _mapper.Map<GetCourse>(course);
            return new AddCoursePayload(response);
        }

        public async Task<AddStudentPayload> AddStudentAsync(AddStudentInput input,
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
