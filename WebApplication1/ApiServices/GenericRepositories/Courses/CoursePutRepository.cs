using ApiDtoLibrary.Courses;
using AutoMapper;
using System;
using WebApplication1.Contracts;
using WebApplication1.Services;

namespace WebApplication1.ApiServices.GenericRepositories.Courses
{
    public class CoursePutRepository : GenericPutRepository<Course, PutCourse>, IGenericPutRepository<Course>
    {
        public CoursePutRepository(IMapper mapper) : base(mapper)
        {
        }

        protected override string GetPathForUpdate(Guid entityId) => $"{_apiPath}/Courses/{entityId}";
    }
}
