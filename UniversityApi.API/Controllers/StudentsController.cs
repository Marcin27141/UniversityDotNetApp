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

        // GET: api/Students/someGuidValue
        [HttpGet("{id}")]
        public async Task<ActionResult<GetStudent>> GetStudent(Guid id)
        {
            var entityStudent = await _repository.GetAsync(id);

            if (entityStudent == null)
            {
                return NotFound();
            }

            var output = _mapper.Map<GetStudent>(entityStudent);
            return output;
        }

        // GET: api/Students/Index/123123
        [HttpGet("IndexCheck/{index}")]
        public async Task<ActionResult<GetStudent>> CheckIfIndexOccupied(string index)
        {
            return Ok(await _repository.IndexIsOccupied(index));
        }

        // GET: api/Students/ByUser/SomeUserId929304
        [HttpGet("User/{id}")]
        public async Task<ActionResult<GetStudent>> GetStudentByUser(string id)
        {
            var entityStudent = await _repository.GetByUserAsync(id);

            if (entityStudent == null)
            {
                return NotFound();
            }

            var output = _mapper.Map<GetStudent>(entityStudent);
            return output;
        }

        // PUT: api/Students
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudent(Guid id, PutStudent putStudent)
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
                await _repository.UpdateWithCoursesAsync(student, putStudent.CoursesIds);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await EntityStudentExists(putStudent.EntityPersonID))
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
            var getStudent = _mapper.Map<GetStudent>(student);

            return Ok(getStudent);
        }

        // DELETE: api/Students/someGuidValue
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEntityStudent(Guid id)
        {
            var entityStudent = await _repository.GetAsync(id);
            if (entityStudent == null)
            {
                return NotFound();
            }

            await _repository.DeleteAsync(id);

            return NoContent();
        }

        // DELETE: api/Students/SomeGuidValue/SomeGuidValue
        [HttpDelete("{studentId}/{courseId}")]
        public async Task<IActionResult> DeleteStudentCourse(Guid studentId, Guid courseId)
        {
            var entityStudent = await _repository.GetAsync(studentId);
            if (entityStudent == null)
            {
                return NotFound();
            }

            await _repository.DeleteStudentsCourseAsync(studentId, courseId);
            return NoContent();
        }

        private async Task<bool> EntityStudentExists(Guid id)
        {
            return await _repository.Exists(id);
        }
    }
}
