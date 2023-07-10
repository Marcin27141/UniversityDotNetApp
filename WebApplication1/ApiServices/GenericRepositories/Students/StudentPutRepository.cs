using ApiDtoLibrary.Courses;
using ApiDtoLibrary.Professors;
using ApiDtoLibrary.Students;
using AutoMapper;
using System;
using WebApplication1.Contracts;
using WebApplication1.Services;
using WebApplication1.Services.People;

namespace WebApplication1.ApiServices.GenericRepositories.Students
{
    public class StudentPutRepository : GenericPutRepository<Student, PutStudent>, IGenericPutRepository<Student>
    {
        public StudentPutRepository(IMapper mapper) : base(mapper)
        {
        }

        protected override string GetPathForUpdate(Guid entityId) => $"{_apiPath}/Students/{entityId}";
    }
}
