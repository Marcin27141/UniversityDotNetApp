using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiDtoLibrary.Students;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniversityApi.API.Contracts;
using UniversityApi.API.DataBase;
using UniversityApi.API.DataBase.Entities;

namespace UniversityApi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentsRepository _repository;
        private readonly IMapper _mapper;

        public StudentsController(IStudentsRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // GET: api/Students
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetStudent>>> GetStudents()
        {
            var students = await _repository.GetAllAsync();
            var output = _mapper.Map<IEnumerable<GetStudent>>(students);
            return Ok(output);
        }

        // GET: api/Students/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetStudent>> GetStudent(int id)
        {
            var entityStudent = await _repository.GetAsync(id);

            if (entityStudent == null)
            {
                return NotFound();
            }

            var output = _mapper.Map<GetStudent>(entityStudent);
            return output;
        }

        // PUT: api/Students/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudent(int id, PutStudent putStudent)
        {
            if (id != putStudent.EntityPersonID)
            {
                return BadRequest("Invalid Record id");
            }

            var student = await _repository.GetAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            _mapper.Map(putStudent, student);

            try
            {
                await _repository.UpdateAsync(student);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await EntityStudentExists(id))
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

        // POST: api/Students
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EntityStudent>> PostStudent(PostStudent postStudent)
        {
            var student = _mapper.Map<EntityStudent>(postStudent);
            await _repository.AddAsync(student);

            return CreatedAtAction(nameof(GetStudent), new { id = student.EntityPersonID }, student);
        }

        // DELETE: api/Students/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEntityStudent(int id)
        {
            var entityStudent = await _repository.GetAsync(id);
            if (entityStudent == null)
            {
                return NotFound();
            }

            await _repository.DeleteAsync(id);

            return NoContent();
        }

        private async Task<bool> EntityStudentExists(int id)
        {
            return await _repository.Exists(id);
        }
    }
}
