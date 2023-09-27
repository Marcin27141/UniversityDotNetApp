using AutoMapper;

namespace UniversityApi.API.GraphQL.Mutations
{
    public partial class Mutation
    {
        private readonly IMapper _mapper;

        public Mutation(IMapper mapper)
        {
            this._mapper = mapper;
        }
    }
}
