using UniversityApi.API.DataBase.Entities;
using UniversityApi.API.DataBase;
using UniversityApi.API.GraphQL.Professors;
using UniversityApi.API.GraphQL.People;

namespace UniversityApi.API.GraphQL.Mutations
{
    public partial class Mutation
    {
        public async Task<DeletePersonPayload> DeletePersonAsync(DeletePersonInput input,
            UniversityApiDbContext dbContext)
        {
            var person = await dbContext.People.FindAsync(input.EntityPersonId);
            if (person != null)
            {
                person.SoftDeleted = true;
                await dbContext.SaveChangesAsync();
                return new DeletePersonPayload(true);
            }
            return new DeletePersonPayload(false);
        }
    }
}
