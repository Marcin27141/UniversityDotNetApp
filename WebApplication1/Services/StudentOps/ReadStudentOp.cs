using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.DataBase;
using WebApplication1.Services.People;

namespace WebApplication1.Services.StudentOps
{
    public class ReadStudentOp : IReadStudentOp
    {
        private readonly AppDbContext _context;
        public ReadStudentOp(AppDbContext context) => _context = context;

        public List<Student> GetAllStudents()
        {
            var output = new List<Student>();
            foreach (var student in _context.Students.Include(s => s.PersonalData))
            {
                output.Add(Student.FromEntityStudent(student));
            }
            return output;
        }

        public Student GetStudentByKey(int studentID)
        {
            var student = _context.Students.AsNoTracking()
                .Include(s => s.PersonalData)
                .Include(s => s.Courses)
                    .ThenInclude(sc => sc.Course)
                        .ThenInclude(c => c.Professor)
                            .ThenInclude(p => p.PersonalData)
                .SingleOrDefault(s => s.EntityStudentID == studentID);
            if (student == null) return null;
            return Student.FromEntityStudent(student);
        }

        public Student GetStudentByIndex(string index)
        {
            var student = _context.Students
                .Include(s => s.PersonalData)
                .Include(s => s.Courses)
                    .ThenInclude(sc => sc.Course)
                        .ThenInclude(c => c.Professor)
                            .ThenInclude(p => p.PersonalData)
                .SingleOrDefault(s => s.StudentIndex.Equals(index));
            if (student == null) return null;
            return Student.FromEntityStudent(student);
        }

        public Student GetStudentByUser(string userId)
        {
            var student = _context.Students
                .Include(s => s.PersonalData)
                    .ThenInclude(pd => pd.ApplicationUser)
                .Include(s => s.Courses)
                    .ThenInclude(sc => sc.Course)
                        .ThenInclude(c => c.Professor)
                            .ThenInclude(p => p.PersonalData)
                .SingleOrDefault(s => s.PersonalData.ApplicationUser.Id.Equals(userId));
            if (student == null) return null;
            return Student.FromEntityStudent(student);
        }

        public List<Course> GetStudentCourses(string studentIndex)
        {
            return _context.Students.Include(st => st.Courses).ThenInclude(sc => sc.Course)
                .Single(st => st.EntityStudentID.Equals(studentIndex))
                .Courses.Select(sc => Course.FromEntityCourse(sc.Course))
                .ToList();
        }
    }

    public interface IReadStudentOp
    {
        Student GetStudentByIndex(string index);
        Student GetStudentByUser(string userId);
    }
}
