using ApiDtoLibrary.GraphQL.Professors;
using UniversityApi.API.DataBase.Entities;
using UniversityApi.API.DataBase;

namespace UniversityApi.API.GraphQL.Mutations
{
    public partial class Mutation
    {
        public async Task<AddProfessorPayload> AddProfessorAsync(AddProfessorInput input,
            UniversityApiDbContext dbContext)
        {
            var professor = _mapper.Map<EntityProfessor>(input);

            await dbContext.AddAsync(professor);
            await dbContext.SaveChangesAsync();

            return new AddProfessorPayload(
                professor.EntityPersonID.ToString(),
                professor.ApplicationUserId.ToString(),
                professor.FirstName,
                professor.LastName,
                professor.PESEL,
                professor.Motherland,
                professor.Birthday,
                professor.IdCode,
                professor.Subject,
                professor.FirstDayAtJob,
                professor.Salary);
        }

        public async Task<UpdateProfessorPayload> UpdateProfessorAsync(UpdateProfessorInput input,
            UniversityApiDbContext dbContext)
        {
            var toUpdate = dbContext.Set<EntityProfessor>().Find(Guid.Parse(input.Id)) ?? throw new KeyNotFoundException($"Professor with id {input.Id} was not found");
            _mapper.Map(input, toUpdate);
            await dbContext.SaveChangesAsync();

            return new UpdateProfessorPayload(toUpdate.EntityPersonID.ToString());
        }
    }
}
