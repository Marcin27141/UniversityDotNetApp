using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication1.Services;

namespace WebApplication1.Contracts
{
    public interface IGradesRepository
    {
        Task<Guid> addGrade(CourseGrade grade);
        Task<CourseGrade> getGrade(Guid gradeId);
        Task<IList<CourseGrade>> getCourseGrades(Guid courseId);
        Task<IList<CourseGrade>> getStudentGrades(Guid studentId);
        Task<Guid> updateGrade(Guid gradeId, float newGradeValue);
        Task<bool> deleteGrade(Guid gradeId);
    }
}
