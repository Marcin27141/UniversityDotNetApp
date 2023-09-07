using UniversityApi.API.DataBase;
using UniversityApi.API.DataBase.Entities;

namespace UniversityApi.API.GraphQL
{
    public class Query
    {
        public IQueryable<EntityProfessor> GetProfessor([Service] UniversityApiDbContext context)
        {
            return context.Set<EntityProfessor>();
        }
    }
}
