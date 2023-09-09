using UniversityApi.API.DataBase.Entities;
using UniversityApi.API.DataBase;
using ApiDtoLibrary.GraphQL.People;

namespace UniversityApi.API.GraphQL.Mutations
{
    public partial class Mutation
    {
        public async Task<DeletePersonPayload> DeletePersonAsync(DeletePersonInput input,
            UniversityApiDbContext dbContext)
        {
            if (!Guid.TryParse(input.Id, out Guid personId))
                return new DeletePersonPayload(false);

            var person = await dbContext.People.FindAsync(personId);
            if (person != null)
            {
                dbContext.Remove(person);
                await dbContext.SaveChangesAsync();
                return new DeletePersonPayload(true);
            }

            return new DeletePersonPayload(false);
        }
    }
}
