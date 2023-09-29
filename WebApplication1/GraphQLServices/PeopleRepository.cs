using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using WebApplication1.Contracts;
using WebApplication1.Services.People;
using GraphQL;
using System.Linq;
using WebApplication1.GraphQLServices.GraphQLDtos;
using WebApplication1.GraphQLServices.QueryGenerators;
using ApiDtoLibrary.Notifications;
using WebApplication1.Services;

namespace WebApplication1.GraphQLServices
{
    public class PeopleRepository : GraphQLRepository, IPeopleRepository
    {
        private readonly IPersonGraphQLQueryGenerator _personQueryGenerator;

        public PeopleRepository(IMapper mapper,
            IAuthenticationRepository authenticationRepository,
            IPersonGraphQLQueryGenerator personQueryGenerator) : base(mapper, authenticationRepository)
        {
            this._personQueryGenerator = personQueryGenerator;
        }

        public List<Person> GetAllPersonalData()
        {
            GraphQLRequest request = _personQueryGenerator.GetQueryForGetAll();
            var response = SendGraphQLRequest(request, () => new { People = new List<GraphQLPersonDto>() }).Result;
            return response.People.Select(_mapper.Map<Person>).ToList();
        }

        public class GraphQLDeletePersonResponse
        {
            public bool WasSuccessful { get; set; }
        }

        public async Task DeleteAsync(Guid personId)
        {
            GraphQLRequest request = _personQueryGenerator.GetQueryForDeleteById(personId);
            await SendGraphQLRequest(request, () => new GraphQLDeletePersonResponse());
        }

        public async Task<Person> GetPerson(Guid id)
        {
            GraphQLRequest request = _personQueryGenerator.GetQueryForGetById(id);
            var response = await SendGraphQLRequest(request, () => new { PersonById = new GraphQLPersonDto() });
            return _mapper.Map<Person>(response.PersonById);
        }

        public async Task<IList<Notification>> GetNotifications(string recipientId)
        {
            GraphQLRequest request = _personQueryGenerator.GetQueryForGetNotifications(recipientId);
            var response = await SendGraphQLRequest(request, () => new { PersonById = new GraphQLPersonDto() });
            return _mapper.Map<List<Notification>>(response.PersonById.Notifications);
        }
    }
}
