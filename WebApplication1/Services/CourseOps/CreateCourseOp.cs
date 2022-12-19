using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.DataBase;
using WebApplication1.Services.People;
using WebApplication1.Services.ProfessorOps;

namespace WebApplication1.Services.CourseOps
{
    public class CreateCourseOp : ICreateCourseOp
    {
        private readonly AppDbContext _context;
        private readonly IReadProfessorOp _readProfessorOp;
        public CreateCourseOp(AppDbContext context, IReadProfessorOp readProfessorOp)
        {
            _context = context;
            _readProfessorOp = readProfessorOp;
        }

        public List<Professor> GetAllProfessors() => _readProfessorOp.GetAllProfessors();

        public Professor GetProfessorByIdCode(string idCode) => _readProfessorOp.GetProfessorByIdCode(idCode);

        public async Task<string> AddCourseAsync(Course course)
        {
            var entityProfessor = _context.Professors.Include(p => p.PersonalData).SingleOrDefault(p => p.IdCode.Equals(course.Professor.IdCode));
            var entityCourse = course.ToEntityCourse(entityProfessor);
            var courseWithSameCode = _context.Courses.IgnoreQueryFilters().SingleOrDefault(c => c.CourseCode.Equals(entityCourse.CourseCode));

            if (courseWithSameCode != null && !courseWithSameCode.SoftDeleted)
                throw new Exception("Course with given code is already added");

            else if (courseWithSameCode != null)             //TODO implement transaction?
            {
                _context.Courses.Remove(courseWithSameCode);
                await _context.SaveChangesAsync();
            }

            _context.Add(entityCourse);
            await _context.SaveChangesAsync();
            return entityCourse.CourseCode;
        }
    }

    public interface ICreateCourseOp
    {
        Task<string> AddCourseAsync(Course course);

        List<Professor> GetAllProfessors();

        Professor GetProfessorByIdCode(string idCode);
    }
}
