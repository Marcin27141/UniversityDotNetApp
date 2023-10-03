using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using WebApplication1.Contracts;
using WebApplication1.Services.People;
using GraphQL;
using System.Linq;
using WebApplication1.Extensions;
using System.Security.Claims;
using WebApplication1.GraphQLServices.GraphQLDtos;
using WebApplication1.GraphQLServices.QueryGenerators;
using ApiDtoLibrary.Students;

namespace WebApplication1.GraphQLServices
{
    public class StudentsRepository : GraphQLRepository, IStudentsRepository
    {
        private readonly IStudentGraphQLQueryGenerator _studentQueryGenerator;

        public StudentsRepository(IMapper mapper, IAuthenticationRepository authenticationRepository, IStudentGraphQLQueryGenerator studentQueryGenerator) : base(mapper, authenticationRepository)
        {
            this._studentQueryGenerator = studentQueryGenerator;
        }

        public List<Student> SortFilterStudents(StudentOrderByOptions orderByOption, StudentFilterByOptions filterByOption, string filter)
        {
            var allStudents = GetAllAsync().Result;
            return allStudents
                .OrderStudentsBy(orderByOption)
                .FilterStudentsBy(filterByOption, filter)
                .ToList();
        }

        public async Task<bool> IndexIsOccupied(string index)
        {
            GraphQLRequest request = _studentQueryGenerator.GetQueryForIndexIsOccupied(index);
            var response = await SendGraphQLRequest(request, () => new { Students = new List<GraphQLStudentDto>() });
            return response.Students.Count > 0;
        }

        public async Task<Student> GetAsync(Guid id)
        {
            GraphQLRequest request = _studentQueryGenerator.GetQueryForGetById(id);
            var response = await SendGraphQLRequest(request, () => new { StudentById = new GraphQLStudentDto() });
            return _mapper.Map<Student>(response.StudentById);
        }

        public async Task<List<Student>> GetAllAsync()
        {
            GraphQLRequest request = _studentQueryGenerator.GetQueryForGetAll();
            var response = await SendGraphQLRequest(request, () => new { Students = new List<GraphQLStudentDto>() });
            return response.Students.Select(_mapper.Map<Student>).ToList();
        }

        public async Task<Student> GetByUserAsync(string userId)
        {
            GraphQLRequest request = _studentQueryGenerator.GetQueryForGetByUser(userId);
            var response = await SendGraphQLRequest(request, () => new { Students = new List<GraphQLStudentDto>() });

            if (response.Students.Count == 0)
                return default;
            var student = response.Students.SingleOrDefault();

            return _mapper.Map<Student>(student);
        }

        public async Task AddClaimAfterPostAsync(string userId, Claim claim)
        {
            await _authenticationRepository.AddClaimAsync(userId, claim);
        }

        public async Task<GetStudent> AddAsync(Student entity)
        {
            var student = await AddStudentWithCoursesAsync(entity, Enumerable.Empty<Guid>());
            return _mapper.Map<GetStudent>(student);
        }

        public async Task<Guid> UpdateAsync(Student entity)
        {
            return await UpdateStudentWithCoursesAsync(entity, Enumerable.Empty<Guid>());
        }

        public class GraphQLDeleteStudentCourseResponse
        {
            public bool WasSuccessful { get; set; }
        }

        public async Task RemoveStudentCourseAsync(Guid studentId, Guid courseId)
        {
            GraphQLRequest request = _studentQueryGenerator.GetQueryForRemoveStudentCourse(studentId, courseId);
            await SendGraphQLRequest(request, () => new GraphQLDeleteStudentCourseResponse());
        }

        public async Task<Student> AddStudentWithCoursesAsync(Student updatedStudent, IEnumerable<Guid> coursesIds)
        {
            GraphQLRequest request = _studentQueryGenerator.GetQueryForAdd(updatedStudent, coursesIds);
            var response = await SendGraphQLRequest(request, () => new { AddStudent = new { GetStudent = new GetStudent() } });

            var getStudent = response.AddStudent.GetStudent;
            await _authenticationRepository.AddEntityPersonIdClaimAsync(getStudent.ApplicationUserId, getStudent.EntityPersonId.ToString());

            return _mapper.Map<Student>(getStudent);
        }

        public async Task<Guid> UpdateStudentWithCoursesAsync(Student updatedStudent, IEnumerable<Guid> coursesIds)
        {
            GraphQLRequest request = _studentQueryGenerator.GetQueryForUpdate(updatedStudent, coursesIds);
            var response = await SendGraphQLRequest(request, () => new { UpdateStudent = new { GetStudent = new GetStudent() } });
            return response.UpdateStudent.GetStudent.EntityPersonId;
        }
    }
}
