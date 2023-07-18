using ApiDtoLibrary.Person;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Data;
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

        // GET: api/People/someGuidValue
        [HttpGet("{id}")]
        public async Task<ActionResult<GetPersonDto>> GetPerson(Guid id)
        {
            var person = await _repository.GetAsync(id);
            var output = _mapper.Map<GetPersonDto>(person);
            return Ok(output);
        }

        // GET: api/People
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetPersonDto>>> GetPeople()
        {
            var people = await _repository.GetAllPersonalDataAsync();
            var output = _mapper.Map<IEnumerable<GetPersonDto>>(people);
            return Ok(output);
        }

        // DELETE: api/People/someGuidValue
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEntityPerson(Guid id)
        {
            var entity = await _repository.GetAsync(id);
            if (entity == null)
            {
                return NotFound();
            }

            await _repository.DeleteAsync(id);

            return NoContent();
        }
    }

    
}
