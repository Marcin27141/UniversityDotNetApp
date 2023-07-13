using ApiDtoLibrary.Courses;
using ApiDtoLibrary.Professors;
using AutoMapper;
using System;
using WebApplication1.ApiServices.BaseRepositories;
using WebApplication1.Contracts;
using WebApplication1.Services;
using WebApplication1.Services.People;

namespace WebApplication1.ApiServices.GenericRepositories.Professors
{
    public class ProfessorPutRepository : GenericPutRepository<Professor, PutProfessor>, IGenericPutRepository<Professor>
    {
        public ProfessorPutRepository(IMapper mapper) : base(mapper)
        {
            _apiPath += ApiPathAppendixDictionary.GetValue(ApiGenericTypes.Professor);
        }
    }
}
