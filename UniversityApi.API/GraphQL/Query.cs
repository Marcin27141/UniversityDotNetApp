using UniversityApi.API.DataBase;
using UniversityApi.API.DataBase.Entities;

namespace UniversityApi.API.GraphQL
{
    public class Query
    {
        [UseFiltering]
        [UseSorting]
        public IQueryable<EntityPerson> GetPerson(UniversityApiDbContext context)
        {
            return context.Set<EntityPerson>();
        }

        [UseFiltering]
        [UseSorting]
        public IQueryable<EntityCourse> GetCourse(UniversityApiDbContext context)
        {
            return context.Courses;
        }

        [UseFiltering]
        [UseSorting]
        public IQueryable<EntityProfessor> GetProfessor(UniversityApiDbContext context)
        {
            return context.Set<EntityProfessor>();
        }

        [UseFiltering]
        [UseSorting]
        public IQueryable<EntityStudent> GetStudent(UniversityApiDbContext context)
        {
            return context.Set<EntityStudent>();
        }
    }
}
