using ApiDtoLibrary.Person;
using AutoMapper;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System;
using WebApplication1.ApiServices.BaseRepositories;
using WebApplication1.Contracts;
using WebApplication1.Services.People;
using GraphQL.Client.Http;
using GraphQL;
using GraphQL.Client.Serializer.Newtonsoft;
using System.Linq;
using static Google.Rpc.Context.AttributeContext.Types;
using WebApplication1.GraphQLServices.GraphQLDtos;

namespace WebApplication1.GraphQLServices
{
    public class PeopleRepository : IPeopleRepository
    {
        private const string GRAPHQL_SERVER_ADDRESS = "https://localhost:7228/graphql/";
        private readonly IMapper _mapper;
        private GraphQLHttpClient _httpClient;

        public PeopleRepository(IMapper mapper)
        {
            this._httpClient = new GraphQLHttpClient(GRAPHQL_SERVER_ADDRESS, new NewtonsoftJsonSerializer());
            this._mapper = mapper;
        }

        public class GraphQLPeopleList
        {
            public List<GraphQLPersonDto> People { get; set; }
        }

        public List<Person> GetAllPersonalData()
        {
            var request = new GraphQLRequest
            {
                Query = @"
                query GetAllPeople {
                    people {
                        Id
                        UserId
                        FirstName
                        LastName
                        PESEL
                        Birthday
                        Motherland
                        PersonStatus
                    }
                }",
            };

            var response = _httpClient.SendQueryAsync<GraphQLPeopleList>(request).Result;

            if (response.Errors != null || response.Data.People == null)
            {
                return new List<Person>();
            }

            var result = response.Data.People.Select(dto => _mapper.Map<Person>(dto)).ToList();
            return result;
        }

        public class GraphQLDeletePerson
        {
            public bool WasSuccessful { get; set; }
        }

        public async Task DeleteAsync(Guid personId)
        {
            var request = new GraphQLRequest
            {
                Query = @"
                mutation DeletePersonById ($id: String) {
                  deletePerson(input: {
                    id: $id
                  })
                  {
                    wasSuccessful
                  }
                }",
                OperationName = "DeletePersonById",
                Variables = new
                {
                    id = personId.ToString()
                }
            };

            await _httpClient.SendQueryAsync<GraphQLDeletePerson>(request);
        }

        public class GraphQLGetPersonById
        {
            public GraphQLPersonDto PersonById { get; set; }
        }

        public async Task<Person> GetPerson(Guid id)
        {
            var request = new GraphQLRequest
            {
                Query = @"
                query GetPersonById($id: String) {
                  personById(id: $id) {
                    Id
                    UserId
                    FirstName
                    LastName
                    PESEL
                    Birthday
                    Motherland
                    PersonStatus
                  }
                }",
                OperationName = "GetPersonById",
                Variables = new
                {
                    id = id.ToString()
                }
            };

            var response = await _httpClient.SendQueryAsync<GraphQLGetPersonById>(request);

            if (response.Errors != null)
            {
                return default;
            }

            return _mapper.Map<Person>(response.Data.PersonById);
        }
    }
}
