﻿using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversityApi.API.Contracts;
using WebApplication1.Contracts;
using WebApplication1.Extensions;
using WebApplication1.Services;

namespace WebApplication1.ApiServices.GenericRepositories.Courses
{
    public class CourseRepository : GenericRepository<Course>, ICoursesRepository
    {
        public CourseRepository(IMapper mapper, IGenericGetRepository<Course> getRepository, IGenericPostRepository<Course> postRepository, IGenericPutRepository<Course> putRepository) : base(mapper, getRepository, postRepository, putRepository)
        {
        }

        public async Task<bool> CourseCodeIsOccupied(string courseCode)
        {
            string checkCourseCodePath = $"{_apiPath}/Course/CourseCode";
            var response = await _httpClient.GetAsync(checkCourseCodePath);
            if (response.IsSuccessStatusCode)
            {
                return bool.Parse(await response.Content.ReadAsStringAsync());
            }
            return default;
        }

        public List<Course> SortFilterCourses(CourseOrderByOptions orderByOption, CourseFilterByOptions filterByOption, string filter)
        {
            var allCourses = GetAllAsync().Result;
            return allCourses
                .OrderCoursesBy(orderByOption)
                .FilterCoursesBy(filterByOption, filter)
                .ToList();
        }

        protected override string GetPathForDelete(object entityId) => $"{_apiPath}/Courses/{entityId}";
    }
}
