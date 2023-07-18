using ApiDtoLibrary.Professors;
using ApiDtoLibrary.Students;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniversityApi.API.Contracts;
using UniversityApi.API.DataBase.Entities;

namespace UniversityApi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfessorsController : ControllerBase
    {
        private readonly IProfessorsRepository _repository;
        private readonly IMapper _mapper;

        public ProfessorsController(IProfessorsRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // GET: api/Professors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetProfessor>>> GetProfessors()
        {
            var professors = await _repository.GetAllAsync();
            var output = _mapper.Map<IEnumerable<GetProfessor>>(professors);
            return Ok(output);
        }

        // GET: api/Professors/someGuidValue
        [HttpGet("{id}")]
        public async Task<ActionResult<GetProfessor>> GetProfessor(Guid id)
        {
            var entityProfessor = await _repository.GetAsync(id);

            if (entityProfessor == null)
            {
                return NotFound();
                //throw new NotFoundException(nameof(GetProfessor), id);
            }

            var output = _mapper.Map<GetProfessor>(entityProfessor);
            return output;
        }

        // GET: api/Professors/IdCode/12312
        [HttpGet("IdCode/{idCode}")]
        public async Task<ActionResult<GetProfessor>> CheckIfIdCodeOccupied(string idCode)
        {
            return Ok(await _repository.IdCodeIsOccupied(idCode));
        }

        // GET: api/Professors/User/SomeUserId93850327
        [HttpGet("User/{id}")]
        public async Task<ActionResult<GetProfessor>> GetProfessorByUser(string id)
        {
            var entityProfessor = await _repository.GetByUserAsync(id);

            if (entityProfessor == null)
            {
                return NotFound();
            }

            var output = _mapper.Map<GetProfessor>(entityProfessor);
            return output;
        }

        // PUT: api/Professors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProfessor(PutProfessor putProfessor)
        {
            var professor = await _repository.GetAsync(putProfessor.EntityPersonID);
            if (professor == null)
            {
                return NotFound();
            }

            _mapper.Map(putProfessor, professor);

            try
            {
                await _repository.UpdateAsync(professor);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await EntityProfessorExists(putProfessor.EntityPersonID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Professors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EntityProfessor>> PostProfessor(PostProfessor postProfessor)
        {
            var entityProfessor = _mapper.Map<EntityProfessor>(postProfessor);
            await _repository.AddAsync(entityProfessor);
            var getProfessor = _mapper.Map<GetProfessor>(entityProfessor);

            return Ok(getProfessor);
        }

        // DELETE: api/Professors/someGuidValue
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEntityProfessor(Guid id)
        {
            var entityProfessor = await _repository.GetAsync(id);
            if (entityProfessor == null)
            {
                return NotFound();
            }

            await _repository.DeleteAsync(id);

            return NoContent();
        }

        private async Task<bool> EntityProfessorExists(Guid id)
        {
            return await _repository.Exists(id);
        }
    }
}
