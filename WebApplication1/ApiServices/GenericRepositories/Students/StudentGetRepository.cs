using ApiDtoLibrary.Courses;
using ApiDtoLibrary.Professors;
using ApiDtoLibrary.Students;
using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using WebApplication1.ApiServices.BaseRepositories;
using WebApplication1.Contracts;
using WebApplication1.Services;
using WebApplication1.Services.People;

namespace WebApplication1.ApiServices.GenericRepositories.Students
{
    public class StudentGetRepository : GenericGetRepository<Student, GetStudent>, IGenericGetRepository<Student>
    {
        public StudentGetRepository(IMapper mapper, IMemoryCache memoryCache) : base(mapper, memoryCache)
        {
            _apiPath += ApiPathAppendixDictionary.GetValue(ApiGenericTypes.Student);
        }

        protected override string GetCacheKeyForGetAll() => "StudentsList";
    }
}
