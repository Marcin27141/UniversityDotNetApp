using ApiDtoLibrary.Courses;
using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using WebApplication1.ApiServices.BaseRepositories;
using WebApplication1.Contracts;
using WebApplication1.Services;

namespace WebApplication1.ApiServices.GenericRepositories.Courses
{
    public class CourseGetRepository : GenericGetRepository<Course, GetCourse>, IGenericGetRepository<Course>
    {
        public CourseGetRepository(IMapper mapper, IMemoryCache memoryCache) : base(mapper, memoryCache)
        {
            _apiPath += ApiPathAppendixDictionary.GetValue(ApiGenericTypes.Course);
        }

        protected override string GetCacheKeyForGetAll() => "CoursesList";
    }
}
