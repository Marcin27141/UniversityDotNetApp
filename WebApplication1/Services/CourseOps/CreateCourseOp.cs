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
        public CreateCourseOp(AppDbContext context, IReadProfessorOp readProfessorOp)
        {
            _context = context;
        }

        public async Task<string> AddCourseAsync(Course course, IEnumerable<string> studentsIndexes)
        {
            var indexToStudentDictionary = _context.Students.ToDictionary(s => s.StudentIndex, s => s);
            var enrolledStudents = studentsIndexes
                .Where(indexToStudentDictionary.ContainsKey)
                .Select(s => indexToStudentDictionary[s])
                .ToList();

            var entityProfessor = _context.Professors.Include(p => p.PersonalData).SingleOrDefault(p => p.IdCode.Equals(course.Professor.IdCode));
            var entityCourse = course.ToEntityCourse(entityProfessor, enrolledStudents);
            var courseWithSameCode = _context.Courses.IgnoreQueryFilters()
                .Include(c => c.Professor)
                .Include(c => c.Students)
                    .ThenInclude(sc => sc.Student)
                .SingleOrDefault(c => c.CourseCode.Equals(entityCourse.CourseCode));

            if (courseWithSameCode != null && !courseWithSameCode.SoftDeleted)
                throw new Exception("Course with given code is already added");

            using (var transaction = _context.Database.BeginTransaction())
            {
                 if (courseWithSameCode != null)
                {
                    _context.Courses.Remove(courseWithSameCode);
                    _context.SaveChanges();
                }

                _context.Add(entityCourse);
                _context.SaveChanges();

                transaction.Commit();
            }

            return entityCourse.CourseCode;
        }
    }

    public interface ICreateCourseOp
    {
        Task<string> AddCourseAsync(Course course, IEnumerable<string> studentsIndexes);
    }
}
