using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.DataBase;
using WebApplication1.DataBase.Entities;
using WebApplication1.Services.People;

namespace WebApplication1.Services.StudentOps
{
    public class UpdateStudentOp : IUpdateStudentOp
    {
        private readonly AppDbContext _context;
        public EntityStudent StudentToUpdate { get; private set; }

        public UpdateStudentOp(AppDbContext context) => _context = context;

        public Student GetStudentToUpdateByIndex(string index)
        {
            StudentToUpdate = _context.Students
                .Include(s => s.PersonalData)
                .ThenInclude(pd => pd.ApplicationUser)
                .Include(s => s.Courses)
                .ThenInclude(sc => sc.Course)
                .ThenInclude(c => c.Professor)
                .ThenInclude(p => p.PersonalData)
                .SingleOrDefault(s => s.StudentIndex.Equals(index));
            if (StudentToUpdate == null) return null;
            return Student.FromEntityStudent(StudentToUpdate);
        }

        public async Task<string> UpdateStudentAsync(Student updatedStudent, IEnumerable<string> coursesCodes)
        {
            var updatedCourses = coursesCodes
                .Select(c => _context.Courses.SingleOrDefault(o => o.CourseCode.Equals(c)))
                .ToList();
            StudentToUpdate = _context.Students.Include(s => s.PersonalData).Include(s => s.Courses).SingleOrDefault(s => s.StudentIndex.Equals(updatedStudent.Index));
            UpdateStudent(updatedStudent, updatedCourses);
            await _context.SaveChangesAsync();
            return StudentToUpdate.StudentIndex;
        }

        private void UpdateStudent(Student updatedStudent, List<EntityCourse> updatedCourses)
        {
            StudentToUpdate.PersonalData.FirstName = updatedStudent.PersonalData.FirstName;
            StudentToUpdate.PersonalData.LastName = updatedStudent.PersonalData.LastName;
            StudentToUpdate.PersonalData.PESEL = updatedStudent.PersonalData.PESEL;
            StudentToUpdate.PersonalData.Birthday = updatedStudent.PersonalData.Birthday;
            StudentToUpdate.PersonalData.Motherland = updatedStudent.PersonalData.Motherland;

            StudentToUpdate.BeginningOfStudying = updatedStudent.BeginningOfStudying;
            StudentToUpdate.Courses = updatedCourses.Select(c => new StudentCourse()
            {
                Student = StudentToUpdate,
                Course = c,
            }).ToList();
        }

        public async Task<bool> RemoveStudentCourseAsync(string index, string courseCode)
        {
            var student = _context.Students.Include(s => s.Courses).ThenInclude(sc => sc.Course).SingleOrDefault(s => s.StudentIndex.Equals(index));
            student.Courses = student.Courses.Where(sc => sc.Student.StudentIndex.Equals(index) && !sc.Course.CourseCode.Equals(courseCode)).ToList();
            await _context.SaveChangesAsync();
            return true;
        }
    }

    public interface IUpdateStudentOp
    {
        Student GetStudentToUpdateByIndex(string index);
        Task<bool> RemoveStudentCourseAsync(string index, string courseCode);
        Task<string> UpdateStudentAsync(Student updatedStudent, IEnumerable<string> coursesCodes);
    }
}
