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
            var professor = _mapper.Map<EntityProfessor>(input.postProfessor);

            await dbContext.AddAsync(professor);
            await dbContext.SaveChangesAsync();

            return new AddProfessorPayload(_mapper.Map<GetProfessor>(professor));
        }

        public async Task<UpdateProfessorPayload> UpdateProfessorAsync(UpdateProfessorInput input,
            UniversityApiDbContext dbContext)
        {
            var putProfessor = input.putProfessor;
            var toUpdate = dbContext.Set<EntityProfessor>().Find(putProfessor.EntityPersonId) ?? throw new KeyNotFoundException($"Professor with id {putProfessor.EntityPersonId} was not found");
            _mapper.Map(putProfessor, toUpdate);
            await dbContext.SaveChangesAsync();

            var getProfessor = _mapper.Map<GetProfessor>(toUpdate);
            return new UpdateProfessorPayload(getProfessor);
        }
    }
}
