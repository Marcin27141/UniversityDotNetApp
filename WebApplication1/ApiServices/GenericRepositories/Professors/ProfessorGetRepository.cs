using ApiDtoLibrary.Courses;
using ApiDtoLibrary.Professors;
using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication1.ApiServices.BaseRepositories;
using WebApplication1.Contracts;
using WebApplication1.Services;
using WebApplication1.Services.People;

namespace WebApplication1.ApiServices.GenericRepositories.Professors
{
    public class ProfessorGetRepository : GenericGetRepository<Professor, GetProfessor>, IGenericGetRepository<Professor>
    {

        public ProfessorGetRepository(IMapper mapper, IMemoryCache memoryCache) : base(mapper, memoryCache)
        {
            _apiPath += ApiPathAppendixDictionary.GetValue(ApiGenericTypes.Professor);
        }

        protected override string GetCacheKeyForGetAll() => "ProfessorsList";
    }
}
