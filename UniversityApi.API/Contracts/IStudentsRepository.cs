using ApiDtoLibrary.Students;
using UniversityApi.API.DataBase.Entities;

namespace UniversityApi.API.Contracts
{
    public interface IStudentsRepository : IGenericRepository<EntityStudent>
    {
        Task<string> UpdateWithCoursesAsync(EntityStudent updatedStudent, IEnumerable<string> coursesCodes);
        Task DeleteStudentsCourseAsync(int id, string courseCode);
    }
}
