using ApiDtoLibrary.Students;
using Microsoft.EntityFrameworkCore;
using UniversityApi.API.DataBase.Entities;

namespace UniversityApi.API.Contracts
{
    public interface IStudentsRepository : IGenericRepository<EntityStudent>
    {
        Task<Guid> UpdateWithCoursesAsync(EntityStudent updatedStudent, IEnumerable<Guid> coursesIds);
        Task DeleteStudentsCourseAsync(Guid studentId, Guid courseId);
    }
}
