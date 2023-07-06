using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using ApiDtoLibrary.Professors;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniversityApi.API.Contracts;
using UniversityApi.API.DataBase;
using UniversityApi.API.DataBase.Entities;
using UniversityApi.API.Exceptions;

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

        // GET: api/Professors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetProfessor>> GetProfessor(int id)
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

        // PUT: api/Professors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProfessor(int id, PutProfessor putProfessor)
        {
            if (id != putProfessor.EntityPersonID)
            {
                return BadRequest("Invalid Record id");
            }

            var professor = await _repository.GetAsync(id);
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
                if (!await EntityProfessorExists(id))
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

            return CreatedAtAction(nameof(GetProfessor), new { id = entityProfessor.EntityPersonID }, entityProfessor);
        }

        [Authorize(Roles = "Administrator")]
        // DELETE: api/Professors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEntityProfessor(int id)
        {
            var entityProfessor = await _repository.GetAsync(id);
            if (entityProfessor == null)
            {
                return NotFound();
            }

            await _repository.DeleteAsync(id);

            return NoContent();
        }

        private async Task<bool> EntityProfessorExists(int id)
        {
            return await _repository.Exists(id);
        }
    }
}
