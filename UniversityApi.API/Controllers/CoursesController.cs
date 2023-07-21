using ApiDtoLibrary.Courses;
using ApiDtoLibrary.Professors;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniversityApi.API.Contracts;
using UniversityApi.API.DataBase.Entities;

namespace UniversityApi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly ICoursesRepository _repository;
        private readonly IMapper _mapper;

        public CoursesController(ICoursesRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // GET: api/Courses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetCourse>>> GetCourses()
        {
            var courses = await _repository.GetAllAsync();
            var output = _mapper.Map<IEnumerable<GetCourse>>(courses);
            return Ok(output);
        }

        // GET: api/Courses/someGuidValue
        [HttpGet("{id}")]
        public async Task<ActionResult<GetCourse>> GetCourse(Guid id)
        {
            var entityCourse = await _repository.GetAsync(id);

            if (entityCourse == null)
            {
                return NotFound();
            }

            var output = _mapper.Map<GetCourse>(entityCourse);

            return output;
        }

        // GET: api/Courses/CourseCode/C01
        [HttpGet("CourseCode/{courseCode}")]
        public async Task<ActionResult<GetCourse>> CheckIfCourseCodeOccupied(string courseCode)
        {
            return Ok(await _repository.CourseCodeIsOccupied(courseCode));
        }

        // PUT: api/Courses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEntityCourse(PutCourse putCourse)
        {
            var course = await _repository.GetAsync(putCourse.EntityCourseId);
            if (course == null)
            {
                return NotFound();
            }

            _mapper.Map(putCourse, course);

            try
            {
                if (putCourse.ProfessorId.HasValue)
                    await _repository.UpdateWithProfessorId(course, putCourse.ProfessorId.Value);
                else
                    course.Professor = null;
                    await _repository.UpdateAsync(course);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await EntityCourseExists(putCourse.EntityCourseId))
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

        // POST: api/Courses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EntityCourse>> PostEntityCourse(PostCourse postCourse)
        {
            var course = _mapper.Map<EntityCourse>(postCourse);
            if (postCourse.ProfessorId is null)
                await _repository.AddAsync(course);
            else
                await _repository.AddWithProfessorId(course, postCourse.ProfessorId.Value);
            var getCourse = _mapper.Map<GetCourse>(course);

            return Ok(getCourse);
        }

        // DELETE: api/Courses/someGuidValue
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEntityCourse(Guid id)
        {
            var entityCourse = await _repository.GetAsync(id);
            if (entityCourse == null)
            {
                return NotFound();
            }

            await _repository.DeleteAsync(id);

            return NoContent();
        }

        private async Task<bool> EntityCourseExists(Guid id)
        {
            return await _repository.Exists(id);
        }
    }
}
