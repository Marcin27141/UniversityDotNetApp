using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication1.Contracts;
using WebApplication1.Queries;
using WebApplication1.Services.People;

namespace UniversityApi.API.Contracts
{
    public interface IStudentsRepository : IGenericRepository<Student>
    {
        List<Student> SortFilterStudents(StudentOrderByOptions orderByOption, StudentFilterByOptions filterByOption, string filter);
        Task<bool> RemoveStudentCourseAsync(int studentId, string courseCode);
        Task<string> UpdateStudentAsync(Student updatedStudent, IEnumerable<string> coursesCodes);
    }
}
