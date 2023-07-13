using ApiDtoLibrary.Courses;
using AutoMapper;
using System;
using WebApplication1.ApiServices.BaseRepositories;
using WebApplication1.Contracts;
using WebApplication1.Services;

namespace WebApplication1.ApiServices.GenericRepositories.Courses
{
    public class CoursePutRepository : GenericPutRepository<Course, PutCourse>, IGenericPutRepository<Course>
    {
        public CoursePutRepository(IMapper mapper) : base(mapper)
        {
            _apiPath += ApiPathAppendixDictionary.GetValue(ApiGenericTypes.Course);
        }
    }
}
