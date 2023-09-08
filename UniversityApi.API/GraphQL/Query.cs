using UniversityApi.API.DataBase;
using UniversityApi.API.DataBase.Entities;

namespace UniversityApi.API.GraphQL
{
    public class Query
    {
        public IQueryable<EntityCourse> GetCourse(UniversityApiDbContext context)
        {
            return context.Courses;
        }

        public IQueryable<EntityProfessor> GetProfessor(UniversityApiDbContext context)
        {
            return context.Set<EntityProfessor>();
        }

        public IQueryable<EntityStudent> GetStudent(UniversityApiDbContext context)
        {
            return context.Set<EntityStudent>();
        }
    }
}
