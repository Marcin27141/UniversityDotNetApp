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

namespace WebApplication1.GraphQLServices
{
    public class PeopleRepository : IPeopleRepository
    {
        private readonly IMapper _mapper;
        private readonly IAuthenticationRepository _authenticationRepository;
        private const string GRAPHQL_SERVER_ADDRESS = "https://localhost:7228/graphql/";
        private GraphQLHttpClient _httpClient;

        public PeopleRepository(IMapper mapper, IAuthenticationRepository authenticationRepository)
        {
            this._mapper = mapper;
            _authenticationRepository = authenticationRepository;
            this._httpClient = new GraphQLHttpClient(GRAPHQL_SERVER_ADDRESS, new NewtonsoftJsonSerializer());
        }

        public class GraphQLPeopleList
        {
            public List<GraphQLGetPersonDto> People { get; set; }
        }

        public class GraphQLGetPersonDto {
            public Guid Id { get; set; }
            public Guid UserId { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string PESEL { get; set; }
            public DateTime Birthday { get; set; }
            public string Motherland { get; set; }
            public PersonStatus PersonStatus { get; set; }

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

            var result = response.Data.People.Select(dto => new Person
            {
                EntityPersonID = dto.Id,
                ApplicationUserId = dto.UserId.ToString(),
                PersonalData = new PersonalData
                {
                    FirstName = dto.FirstName,
                    LastName = dto.LastName,
                    PESEL = dto.PESEL,
                    Birthday = dto.Birthday,
                    Motherland = dto.Motherland
                },
                PersonStatus = dto.PersonStatus
            }).ToList();
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
            //if (response.Errors != null)
            //    return false;
            //else
            //    return response.Data.WasSuccessful; 
        }

        public class GraphQLGetPersonById
        {
            public GraphQLGetPersonDto PersonById { get; set; }
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

            var result = new Person
            {
                EntityPersonID = response.Data.PersonById.Id,
                ApplicationUserId = response.Data.PersonById.UserId.ToString(),
                PersonalData = new PersonalData
                {
                    FirstName = response.Data.PersonById.FirstName,
                    LastName = response.Data.PersonById.LastName,
                    PESEL = response.Data.PersonById.PESEL,
                    Birthday = response.Data.PersonById.Birthday,
                    Motherland = response.Data.PersonById.Motherland
                },
                PersonStatus = response.Data.PersonById.PersonStatus
            };
            return result;
        }
    }
}
