using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiDtoLibrary.Courses;
using AutoMapper;
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

        // GET: api/Courses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetCourse>> GetCourse(int id)
        {
            var entityCourse = await _repository.GetAsync(id);

            if (entityCourse == null)
            {
                return NotFound();
            }

            var output = _mapper.Map<GetCourse>(entityCourse);

            return output;
        }

        // PUT: api/Courses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEntityCourse(int id, PutCourse putCourse)
        {
            if (id != putCourse.EntityCourseID)
            {
                return BadRequest();
            }

            var course = await _repository.GetAsync(id);
            if (course == null)
            {
                return NotFound();
            }

            _mapper.Map(putCourse, course);

            try
            {
                await _repository.UpdateAsync(course);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await EntityCourseExists(id))
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
            await _repository.AddAsync(course);

            return CreatedAtAction(nameof(GetCourse), new { id = course.EntityCourseID }, course);
        }

        // DELETE: api/Courses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEntityCourse(int id)
        {
            var entityCourse = await _repository.GetAsync(id);
            if (entityCourse == null)
            {
                return NotFound();
            }

            await _repository.DeleteAsync(id);

            return NoContent();
        }

        private async Task<bool> EntityCourseExists(int id)
        {
            return await _repository.Exists(id);
        }
    }
}
