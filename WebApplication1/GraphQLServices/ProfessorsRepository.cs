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
using WebApplication1.Extensions;
using System.Security.Claims;
using ApiDtoLibrary.Professors;
using static WebApplication1.GraphQLServices.PeopleRepository;
using Azure;
using ApiDtoLibrary.GraphQL.Professors;

namespace WebApplication1.GraphQLServices
{
    public class ProfessorsRepository : IProfessorsRepository
    {
        private readonly IMapper _mapper;
        private IAuthenticationRepository _authenticationRepository;
        private const string GRAPHQL_SERVER_ADDRESS = "https://localhost:7228/graphql/";
        private GraphQLHttpClient _httpClient;

        public ProfessorsRepository(IMapper mapper, IAuthenticationRepository authenticationRepository)
        {
            this._mapper = mapper;
            _authenticationRepository = authenticationRepository;
            this._httpClient = new GraphQLHttpClient(GRAPHQL_SERVER_ADDRESS, new NewtonsoftJsonSerializer());
        }

        public class GraphQLResponse<T>
        {
            public T Data { get; set; }
        }

        public class GraphQLProfessorsList
        {
            public List<GraphQLGetProfessorDto> Professors { get; set; }
        }

        public class GraphQLGetProfessorById
        {
            public GraphQLGetProfessorDto ProfessorById { get; set; }
        }

        public class GraphQLGetProfessorDto
        {
            public Guid Id { get; set; }
            public Guid UserId { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string PESEL { get; set; }
            public DateTime Birthday { get; set; }
            public string Motherland { get; set; }
            public PersonStatus PersonStatus { get; set; }
            public string ProfessorId { get; set; }
            public string Subject { get; set; }
            public DateTime FirstDayAtJob { get; set; }
            public int Salary { get; set; }

        }


        //public async Task DeleteAsync(Guid personId)
        //{
        //    var request = new GraphQLRequest
        //    {
        //        Query = @"
        //        mutation DeletePersonById ($id: String) {
        //          deletePerson(input: {
        //            id: $id
        //          })
        //          {
        //            wasSuccessful
        //          }
        //        }",
        //        OperationName = "DeletePersonById",
        //        Variables = new
        //        {
        //            id = personId.ToString()
        //        }
        //    };

        //    await _httpClient.SendQueryAsync<GraphQLDeletePerson>(request);
        //    //if (response.Errors != null)
        //    //    return false;
        //    //else
        //    //    return response.Data.WasSuccessful; 
        //}

        public List<Professor> SortFilterProfessors(ProfessorOrderByOptions orderByOption, ProfessorFilterByOptions filterByOption, string filter)
        {
            var allProfessors = GetAllAsync().Result;
            return allProfessors
                .OrderProfessorsBy(orderByOption)
                .FilterProfessorsBy(filterByOption, filter)
                .ToList();
        }

        public Task<bool> IdCodeIsOccupied(string idCode)
        {
            //throw new NotImplementedException();
            return Task.FromResult(false);
        }

        public Task DeleteAsync(Professor entity)
        {
            throw new NotImplementedException();
        }

        public async Task<Professor> GetAsync(Guid id)
        {
            var request = new GraphQLRequest
            {
                Query = @"
                query GetProfessorById($id: String) {
                  professorById(id: $id) {
                    Id
                    UserId
                    FirstName
                    LastName
                    PESEL
                    Birthday
                    Motherland
                    PersonStatus
                    ProfessorId
                    Subject
                    FirstDayAtJob
                    Salary
                  }
                }",
                OperationName = "GetProfessorById",
                Variables = new
                {
                    id = id.ToString()
                }
            };

            var response = await _httpClient.SendQueryAsync<GraphQLGetProfessorById>(request);

            if (response.Errors != null)
            {
                return default;
            }

            var result = new Professor
            {
                EntityPersonID = response.Data.ProfessorById.Id,
                ApplicationUserId = response.Data.ProfessorById.UserId.ToString(),
                PersonalData = new PersonalData
                {
                    FirstName = response.Data.ProfessorById.FirstName,
                    LastName = response.Data.ProfessorById.LastName,
                    PESEL = response.Data.ProfessorById.PESEL,
                    Birthday = response.Data.ProfessorById.Birthday,
                    Motherland = response.Data.ProfessorById.Motherland
                },
                PersonStatus = response.Data.ProfessorById.PersonStatus,
                IdCode = response.Data.ProfessorById.ProfessorId,
                Subject = response.Data.ProfessorById.Subject,
                FirstDayAtJob = response.Data.ProfessorById.FirstDayAtJob,
                Salary = response.Data.ProfessorById.Salary
            };
            return result;
        }

        public async Task<List<Professor>> GetAllAsync()
        {
            var request = new GraphQLRequest
            {
                Query = @"
                query GetAlProfessors {
                    professors {
                        Id
                        UserId
                        FirstName
                        LastName
                        PESEL
                        Birthday
                        Motherland
                        PersonStatus
                        ProfessorId
                        Subject
                        FirstDayAtJob
                        Salary
                    }
                }",
            };

            var response = await _httpClient.SendQueryAsync<GraphQLProfessorsList>(request);

            if (response.Errors != null || response.Data.Professors == null)
            {
                return new List<Professor>();
            }

            var result = response.Data.Professors.Select(dto => new Professor
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
                PersonStatus = dto.PersonStatus,
                IdCode = dto.ProfessorId,
                Subject = dto.Subject,
                FirstDayAtJob = dto.FirstDayAtJob,
                Salary = dto.Salary
            }).ToList();
            return result;
        }

        public async Task<Professor> GetByUserAsync(string userId)
        {
            //var request = new GraphQLRequest
            //{
            //    Query = @"
            //    query GetProfessorByUser($userId: String) {
            //      personById(where: {UserId: {eq: $userId}}) {
            //        Id
            //        UserId
            //        FirstName
            //        LastName
            //        PESEL
            //        Birthday
            //        Motherland
            //        PersonStatus
            //        ProfessorId
            //        Subject
            //        FirstDayAtJob
            //        Salary
            //      }
            //    }",
            //    OperationName = "GetProfessorByUser",
            //    Variables = new
            //    {
            //        userId = userId
            //    }
            //};

            //var response = await _httpClient.SendQueryAsync<GraphQLGetProfessorDto>(request);

            //if (response.Errors != null)
            //{
            //    return default;
            //}

            //var result = new Professor
            //{
            //    EntityPersonID = response.Data.Id,
            //    ApplicationUserId = response.Data.UserId.ToString(),
            //    PersonalData = new PersonalData
            //    {
            //        FirstName = response.Data.FirstName,
            //        LastName = response.Data.LastName,
            //        PESEL = response.Data.PESEL,
            //        Birthday = response.Data.Birthday,
            //        Motherland = response.Data.Motherland
            //    },
            //    PersonStatus = response.Data.PersonStatus,
            //    IdCode = response.Data.ProfessorId,
            //    Subject = response.Data.Subject,
            //    FirstDayAtJob = response.Data.FirstDayAtJob,
            //    Salary = response.Data.Salary
            //};
            //return result;
            //throw new NotImplementedException();
            return null;
        }

        public async Task AddClaimAfterPostAsync(string userId, Claim claim)
        {
            //var entityPersonIdClaim = new Claim("EntityPersonId", mappedResult.EntityPersonID.ToString());
            //await base.AddClaimAfterPostAsync(mappedResult.ApplicationUserId, entityPersonIdClaim);
            await _authenticationRepository.AddClaimAsync(userId, claim);
        }

        public class AddProfessorResponse
        {
            public AddProfessorPayload AddProfessor { get; set; }
        }

        public class AddTestResponse
        {
            public bool WasSuccessful { get; set; }
        }

        public async Task<GetProfessor> AddAsync(Professor entity)
        {
            //var request = new GraphQLRequest
            //{
            //    Query = @"
            //    mutation TestMutation {
            //      addTest (input: { successful: true })
            //      {
            //        wasSuccessful
            //      }
            //    }",
            //};

            //var response = await _httpClient.SendQueryAsync<AddTestResponse>(request);

            //if (response.Errors != null)
            //{
            //    return default;
            //}

            //return null;

            var request = new GraphQLRequest
            {
                Query = @"
                mutation AddProfessorMutation($input: AddProfessorInput) {
                  addProfessor (input: $input)
                  {
                    id
                    userId
                    firstName
                    lastName
                    pesel
                    motherland
                    birthday
                    professorId
                    subject
                    firstDayAtJob
                    salary
                  }
                }",
                Variables = new
                {
                    input = new AddProfessorInput(
                        entity.ApplicationUserId.ToString(),
                        entity.PersonalData.FirstName,
                        entity.PersonalData.LastName,
                        entity.PersonalData.PESEL,
                        entity.PersonalData.Motherland,
                        entity.PersonalData.Birthday,
                        entity.IdCode,
                        entity.Subject,
                        entity.FirstDayAtJob,
                        entity.Salary
                    )
                }
            };

            var response = await _httpClient.SendQueryAsync<AddProfessorResponse>(request);

            if (response.Errors != null)
            {
                return default;
            }

            var payload = response.Data.AddProfessor;
            var result = new GetProfessor
            {
                EntityPersonId = Guid.Parse(payload.Id),
                ApplicationUserId = payload.UserId,
                FirstName = payload.FirstName,
                LastName = payload.LastName,
                IdCode = payload.ProfessorId,
                Subject = payload.Subject,
            };
            return result;
            //throw new NotImplementedException();
        }

        public Task<Guid> UpdateAsync(Professor updatedEntity)
        {
            throw new NotImplementedException();
        }
    }
}
