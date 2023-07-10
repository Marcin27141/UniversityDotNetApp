using ApiDtoLibrary.Person;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<ActionResult<IEnumerable<GetPersonDto>>> GetPeople()
        {
            var people = await _repository.GetAllPersonalDataAsync();
            var output = _mapper.Map<IEnumerable<GetPersonDto>>(people);
            return Ok(output);
        }
    }

    
}
