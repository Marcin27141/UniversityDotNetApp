using ApiDtoLibrary;
using ApiDtoLibrary.Professors;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;
using UniversityApi.API.Contracts;

namespace UniversityApi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly IPeopleRespository _repository;
        private readonly IMapper _mapper;
        public PeopleController(IPeopleRespository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // GET: api/People
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonDto>>> GetPeople()
        {
            var people = await _repository.GetAllPersonalDataAsync();
            var output = _mapper.Map<IEnumerable<PersonDto>>(people);
            return Ok(output);
        }
    }

    
}
