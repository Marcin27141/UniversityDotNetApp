using Microsoft.EntityFrameworkCore;
using UniversityApi.API.DataBase.Entities;

namespace UniversityApi.API.Contracts
{
    public interface ICoursesRepository : IGenericRepository<EntityCourse>
    {
        Task<bool> CourseCodeIsOccupied(string courseCode);
        Task<EntityCourse> AddWithProfessorId(EntityCourse entity, Guid professorId);
    }
}
