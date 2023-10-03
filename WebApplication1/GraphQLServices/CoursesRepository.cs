using ApiDtoLibrary.Courses;
using AutoMapper;
using GraphQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApplication1.Contracts;
using WebApplication1.Extensions;
using WebApplication1.GraphQLServices.GraphQLDtos;
using WebApplication1.GraphQLServices.QueryGenerators;
using WebApplication1.Services;

namespace WebApplication1.GraphQLServices
{
    public class CoursesRepository : GraphQLRepository, ICoursesRepository
    {
        private readonly ICourseGraphQLQueryGenerator _courseQueryGenerator;

        public CoursesRepository(IMapper mapper,
            IAuthenticationRepository authenticationRepository,
            ICourseGraphQLQueryGenerator courseQueryGenerator) : base(mapper, authenticationRepository)
        {
            this._courseQueryGenerator = courseQueryGenerator;
        }

        public async Task<GetCourse> AddAsync(Course entity)
        {
            GraphQLRequest request = _courseQueryGenerator.GetQueryForAdd(entity);
            var response = await SendGraphQLRequest(request, () => new { AddCourse = new { GetCourse = new GetCourse() } });
            return response.AddCourse.GetCourse;
        }

        public async Task AddClaimAfterPostAsync(string userId, Claim claim)
        {
            await _authenticationRepository.AddClaimAsync(userId, claim);
        }

        public async Task<bool> CourseCodeIsOccupied(string courseCode)
        {
            GraphQLRequest request = _courseQueryGenerator.GetQueryForCodeIsOccupied(courseCode);
            var response = await SendGraphQLRequest(request, () => new { Courses = new List<GraphQLCourseDto>() });
            return response.Courses.Count > 0;
        }

        public class GraphQLDeleteCourseResponse
        {
            public bool WasSuccessful { get; set; }
        }

        public async Task<bool> DeleteAsync(Course course)
        {
            GraphQLRequest request = _courseQueryGenerator.GetQueryForDeleteById(course.EntityCourseID);
            await SendGraphQLRequest(request, () => new GraphQLDeleteCourseResponse());
            return true;
        }

        public async Task<List<Course>> GetAllAsync()
        {
            GraphQLRequest request = _courseQueryGenerator.GetQueryForGetAll();
            var response = await SendGraphQLRequest(request, () => new { Courses = new List<GraphQLCourseDto>() });
            return response.Courses.Select(_mapper.Map<Course>).ToList();
        }

        public async Task<Course> GetAsync(Guid id)
        {
            GraphQLRequest request = _courseQueryGenerator.GetQueryForGetById(id);
            var response = await SendGraphQLRequest(request, () => new { CourseById = new GraphQLCourseDto() });
            return _mapper.Map<Course>(response.CourseById);
        }

        public Task<Course> GetByUserAsync(string userId)
        {
            throw new InvalidOperationException();
        }

        public List<Course> SortFilterCourses(CourseOrderByOptions orderByOption, CourseFilterByOptions filterByOption, string filter)
        {
            var allCourses = GetAllAsync().Result;
            return allCourses
                .OrderCoursesBy(orderByOption)
                .FilterCoursesBy(filterByOption, filter)
                .ToList();
        }

        public async Task<Guid> UpdateAsync(Course updatedEntity)
        {
            GraphQLRequest request = _courseQueryGenerator.GetQueryForUpdate(updatedEntity);
            var response = await SendGraphQLRequest(request, () => new { UpdateCourse = new { GetCourse = new GetCourse() } });
            return response.UpdateCourse.GetCourse.EntityCourseId;
        }
    }
}
