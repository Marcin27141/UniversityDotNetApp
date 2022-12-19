using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.DataBase;
using WebApplication1.Services.People;

namespace WebApplication1.Services.StudentOps
{
    public class CreateStudentOp : ICreateStudentOp
    {
        private readonly AppDbContext _context;

        public CreateStudentOp(AppDbContext context) => _context = context;

        public List<Course> GetAvailableCourses()
        {
            return _context.Courses
                .Include(c => c.Professor)
                .ThenInclude(p => p.PersonalData)
                .AsNoTracking()
                .Select(c => Course.FromEntityCourse(c))
                .ToList();
        }
        public async Task<string> AddStudentAsync(Student student, IEnumerable<string> coursesCodes)
        {
            var codeToCourseDictionary = _context.Courses.Include(c => c.Professor).ThenInclude(p => p.PersonalData).ToDictionary(c => c.CourseCode, c => c);
            var chosenCourses = coursesCodes
                .Where(c => codeToCourseDictionary.ContainsKey(c))
                .Select(c => codeToCourseDictionary[c])
                .ToList();
            var entityStudent = student.ToEntityStudent(chosenCourses);

            var studentWithSameId = _context.Students.IgnoreQueryFilters().SingleOrDefault(s => s.StudentIndex.Equals(entityStudent.StudentIndex));

            if (studentWithSameId != null && !studentWithSameId.SoftDeleted)
                throw new Exception("Student with given index is already added");

            else if (studentWithSameId != null)         //TODO implement transaction?
            {
                _context.Remove(studentWithSameId);
                await _context.SaveChangesAsync();
            }

            _context.Add(entityStudent);
            await _context.SaveChangesAsync();
            return entityStudent.StudentIndex;
        }
    }

    public interface ICreateStudentOp
    {
        List<Course> GetAvailableCourses();
        Task<string> AddStudentAsync(Student student, IEnumerable<string> coursesIds);
    }
}
