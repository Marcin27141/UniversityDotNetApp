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
            public List<GraphQLGetPersonDto> Person { get; set; }
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
                query People {
                    person {
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

            if (response.Errors != null)
            {
                return new List<Person>();
            }

            var result = response.Data.Person.Select(dto => new Person
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

        public async Task DeleteAsync(Person person)
        {
            //string deletePath = $"{_apiPath}/{person.EntityPersonID}";
            //await _httpClient.DeleteAsync(deletePath);
            //await _authenticationRepository.RemoveClaimAsync(person.ApplicationUserId, "EntityPersonId");
            
        }

        public async Task<Person> GetPerson(Guid id)
        {
            //string getPath = $"{_apiPath}/{id}";
            //var response = await _httpClient.GetAsync(getPath);
            //if (response.IsSuccessStatusCode)
            //{
            //    var getResult = await response.Content.ReadFromJsonAsync<GetPersonDto>();
            //    var result = _mapper.Map<Person>(getResult);
            //    return result;
            //}
            //return default;
            return default;
        }
    }
}
