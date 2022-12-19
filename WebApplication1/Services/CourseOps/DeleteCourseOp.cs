using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.DataBase;

namespace WebApplication1.Services.CourseOps
{
    public class DeleteCourseOp : IDeleteCourseOp
    {
        private readonly AppDbContext _context;
        public DeleteCourseOp(AppDbContext context) => _context = context;

        public async Task DeleteCourseByCodeAsync(string courseCode)
        {
            var entityCourse = _context.Courses.SingleOrDefault(c => c.CourseCode.Equals(courseCode));
            if (entityCourse == null)
                throw new Exception("Course doesn't exist");
            entityCourse.SoftDeleted = true;
            await _context.SaveChangesAsync();
        }
    }

    public interface IDeleteCourseOp
    {
        Task DeleteCourseByCodeAsync(string courseCode);
    }
}
