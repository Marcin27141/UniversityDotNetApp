using Microsoft.CodeAnalysis.Text;
using Microsoft.EntityFrameworkCore;
using UniversityApi.API.DataBase;
using UniversityApi.API.DataBase.Entities;

namespace UniversityApi.API.GraphQL
{
    public class Query
    {
        [UseFiltering]
        [UseSorting]
        public IQueryable<EntityPerson> GetPeople(UniversityApiDbContext context)
        {
            return context.People;            
        }

        public EntityPerson GetPersonById(UniversityApiDbContext context, string id)
        {
            if (Guid.TryParse(id, out Guid personId))
                return context.Set<EntityPerson>().Find(personId);
            else
                return default;
        }

        [UseFiltering]
        [UseSorting]
        public IQueryable<EntityCourse> GetCourses(UniversityApiDbContext context)
        {
            return context.Courses;
        }

        public EntityCourse GetCourseById(UniversityApiDbContext context, string id)
        {
            if (Guid.TryParse(id, out Guid courseId))
                return context.Courses.Find(courseId);
            else
                return default;
        }

        [UseFiltering]
        [UseSorting]
        public IQueryable<EntityProfessor> GetProfessors(UniversityApiDbContext context)
        {
            return context.Set<EntityProfessor>();
        }

        public EntityProfessor GetProfessorById(UniversityApiDbContext context, string id)
        {
            if (Guid.TryParse(id, out Guid personId))
                return context.Set<EntityProfessor>().Find(personId);
            else
                return default;
        }

        [UseFiltering]
        [UseSorting]
        public IQueryable<EntityStudent> GetStudents(UniversityApiDbContext context)
        {
            return context.Set<EntityStudent>();
        }

        public EntityStudent GetStudentById(UniversityApiDbContext context, string id)
        {
            if (Guid.TryParse(id, out Guid personId))
                return context.Set<EntityStudent>().Find(personId);
            else
                return default;
        }
    }
}
