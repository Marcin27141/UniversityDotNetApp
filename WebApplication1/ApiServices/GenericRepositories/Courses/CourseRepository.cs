﻿using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversityApi.API.Contracts;
using WebApplication1.ApiServices.BaseRepositories;
using WebApplication1.Contracts;
using WebApplication1.Extensions;
using WebApplication1.Services;

namespace WebApplication1.ApiServices.GenericRepositories.Courses
{
    public class CourseRepository : GenericRepository<Course>, ICoursesRepository
    {
        public CourseRepository(IMapper mapper, IGenericGetRepository<Course> getRepository, IGenericPostRepository<Course> postRepository, IGenericPutRepository<Course> putRepository) : base(mapper, getRepository, postRepository, putRepository)
        {
            _apiPath += ApiPathAppendixDictionary.GetValue(ApiGenericTypes.Course);
        }

        public async Task<bool> CourseCodeIsOccupied(string courseCode)
        {
            string checkCourseCodePath = $"{_apiPath}/CourseCode";
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
    }
}
