using ApiDtoLibrary.GraphQL.Professors;
using UniversityApi.API.DataBase.Entities;
using UniversityApi.API.DataBase;
using ApiDtoLibrary.Professors;

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
            var toUpdate = dbContext.Set<EntityProfessor>().Find(input.putProfessor.EntityPersonId) ?? throw new KeyNotFoundException($"Professor with id {input.putProfessor.EntityPersonId} was not found");
            _mapper.Map(input.putProfessor, toUpdate);
            await dbContext.SaveChangesAsync();

            var getProfessor = _mapper.Map<GetProfessor>(toUpdate);
            return new UpdateProfessorPayload(getProfessor);
        }
    }
}
