using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.DataBase;

namespace WebApplication1.Services.CourseOps
{
    public class UpdateCourseOp : IUpdateCourseOp
    {
        private readonly AppDbContext _context;
        public DataBase.Entities.Course CourseToUpdate { get; private set; }
        //public Dictionary<string, DataBase.Entities.Course> AvailableCoursesByCode { get; private set; }

        public UpdateCourseOp(AppDbContext context) => _context = context;

        public Course GetCourseToUpdateByCode(string code)
        {
            CourseToUpdate = _context.Courses
                .Include(c => c.Professor)
                .ThenInclude(p => p.PersonalData)
                .SingleOrDefault(c => c.CourseCode.Equals(code));
            if (CourseToUpdate == null) return null;
            return Course.FromEntityCourse(CourseToUpdate);
        }

        public async Task<string> UpdateCourseAsync(Course updatedCourse)
        {
            var entityProfessor = _context.Professors.Include(p => p.PersonalData).SingleOrDefault(p => p.IdCode.Equals(updatedCourse.Professor.IdCode));
            var updatedEntity = updatedCourse.ToEntityCourse(entityProfessor);
            CourseToUpdate = _context.Courses.SingleOrDefault(c => c.CourseCode.Equals(updatedEntity.CourseCode));
            UpdateCourse(updatedEntity);
            await _context.SaveChangesAsync();
            return CourseToUpdate.CourseCode;
        }

        private void UpdateCourse(DataBase.Entities.Course updatedCourse)
        {
            CourseToUpdate.Name = updatedCourse.Name;
            CourseToUpdate.ECTS = updatedCourse.ECTS;
            CourseToUpdate.IsFinishedWithExam = updatedCourse.IsFinishedWithExam;
            CourseToUpdate.Professor = updatedCourse.Professor;
        }
    }

    public interface IUpdateCourseOp
    {
        Course GetCourseToUpdateByCode(string code);
        Task<string> UpdateCourseAsync(Course updatedCourse);
    }
}
