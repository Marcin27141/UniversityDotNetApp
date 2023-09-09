using ApiDtoLibrary.Students;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication1.Contracts;
using WebApplication1.Extensions;
using WebApplication1.Services.People;

namespace WebApplication1.Contracts
{
    public interface IStudentsRepository : IGenericRepository<Student, GetStudent>
    {
        List<Student> SortFilterStudents(StudentOrderByOptions orderByOption, StudentFilterByOptions filterByOption, string filter);
        Task RemoveStudentCourseAsync(Guid studentId, Guid courseId);
        Task<Student> AddStudentWithCoursesAsync(Student updatedStudent, IEnumerable<Guid> coursesIds);
        Task<Guid> UpdateStudentWithCoursesAsync(Student updatedStudent, IEnumerable<Guid> coursesIds);
        Task<bool> IndexIsOccupied(string index);
    }
}
