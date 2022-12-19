using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.DataBase;
using WebApplication1.Services.People;

namespace WebApplication1.Services.StudentOps
{
    public class UpdateStudentOp : IUpdateStudentOp
    {
        private readonly AppDbContext _context;
        public DataBase.Entities.Student StudentToUpdate { get; private set; }
        public Dictionary<string, DataBase.Entities.Course> AvailableCoursesByCode { get; private set; }

        public UpdateStudentOp(AppDbContext context) => _context = context;

        public Student GetStudentToUpdateByIndex(string index)
        {
            StudentToUpdate = _context.Students
                .Include(s => s.PersonalData)
                .Include(s => s.Courses)
                .ThenInclude(sc => sc.Course)
                .SingleOrDefault(s => s.StudentIndex.Equals(index));
            if (StudentToUpdate == null) return null;
            return Student.FromEntityStudent(StudentToUpdate);
        }

        public List<Course> GetAvailableCourses()
        {
            AvailableCoursesByCode = _context.Courses.AsNoTracking().ToDictionary(c => c.CourseCode, c => c);
            return AvailableCoursesByCode.Values.Select(c => Course.FromEntityCourse(c)).ToList();
        }

        public async Task<string> UpdateStudentAsync(Student updatedStudent, IEnumerable<string> coursesCodes)
        {
            var updatedCourses = coursesCodes
                .Where(c => AvailableCoursesByCode.ContainsKey(c))
                .Select(c => AvailableCoursesByCode[c])
                .ToList();
            var updatedEntity = updatedStudent.ToEntityStudent(updatedCourses);
            UpdateStudent(updatedEntity);
            await _context.SaveChangesAsync();
            return StudentToUpdate.StudentIndex;
        }

        private void UpdateStudent(DataBase.Entities.Student updatedStudent)
        {
            StudentToUpdate.PersonalData.FirstName = updatedStudent.PersonalData.FirstName;
            StudentToUpdate.PersonalData.LastName = updatedStudent.PersonalData.LastName;
            StudentToUpdate.PersonalData.PESEL = updatedStudent.PersonalData.PESEL;
            StudentToUpdate.PersonalData.Birthday = updatedStudent.PersonalData.Birthday;
            StudentToUpdate.PersonalData.Motherland = updatedStudent.PersonalData.Motherland;

            StudentToUpdate.Average = updatedStudent.Average;
            StudentToUpdate.BeginningOfStudying = updatedStudent.BeginningOfStudying;
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
        List<Course> GetAvailableCourses();
        Task<bool> RemoveStudentCourseAsync(string index, string courseCode);
        Task<string> UpdateStudentAsync(Student updatedStudent, IEnumerable<string> coursesCodes);
    }
}
