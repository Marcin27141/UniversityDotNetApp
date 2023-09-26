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
using WebApplication1.GraphQLServices.GraphQLDtos;
using static Grpc.Core.Metadata;
using GraphQL.Client.Abstractions;


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

        public class GraphQLProfessorsList
        {
            public List<GraphQLProfessorDto> Professors { get; set; }
        }

        public class GraphQLGetProfessorById
        {
            public GraphQLProfessorDto ProfessorById { get; set; }
        }

        public List<Professor> SortFilterProfessors(ProfessorOrderByOptions orderByOption, ProfessorFilterByOptions filterByOption, string filter)
        {
            var allProfessors = GetAllAsync().Result;
            return allProfessors
                .OrderProfessorsBy(orderByOption)
                .FilterProfessorsBy(filterByOption, filter)
                .ToList();
        }

        public async Task<bool> IdCodeIsOccupied(string idCode)
        {
            var request = new GraphQLRequest
            {
                Query = @"
                query GetProfessorById($idCodeVar: String) {
                  professors(where: {idCode: {eq: $idCodeVar}}) {
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
                Variables = new
                {
                    idCodeVar = idCode
                }
            };

            var response = await _httpClient.SendQueryAsync<GraphQLProfessorsList>(request);

            if (response.Errors != null)
            {
                return false;
            }

            return response.Data.Professors.Count > 0;
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

            var result = response.Data.Professors.Select(dto => _mapper.Map<Professor>(dto)).ToList();
            return result;
        }

        public async Task<Professor> GetByUserAsync(string userId)
        {
            var request = new GraphQLRequest
            {
                Query = @"
                query GetProfessorByUser($userIdVar: UUID) {
                    professors(where: {applicationUserId: {eq: $userIdVar}}) {
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
                Variables = new
                {
                    userIdVar = userId
                }
            };

            var response = await _httpClient.SendQueryAsync<GraphQLProfessorsList>(request);

            if (response.Errors != null)
            {
                return default;
            }

            if (response.Data.Professors.Count == 0)
                return default;
            var professor = response.Data.Professors.SingleOrDefault();

            return _mapper.Map<Professor>(professor);
        }

        public async Task AddClaimAfterPostAsync(string userId, Claim claim)
        {
            await _authenticationRepository.AddClaimAsync(userId, claim);
        }

        public class AddProfessorResponse
        {
            public AddProfessorPayload AddProfessor { get; set; }
        }

        public async Task<GetProfessor> AddAsync(Professor entity)
        {
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
            //var response = await _httpClient.SendQueryAsync(request, () => new { AddProfessor = new { GetProfessor = new GetProfessor() } });

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
            //return response.Data.AddProfessor.GetProfessor;
        }

        public async Task<Guid> UpdateAsync(Professor entity)
        {
            var request = new GraphQLRequest
            {
                Query = @"
                mutation UpdateProfessorMutation($input: UpdateProfessorInput) {
                  updateProfessor (input: $input)
                  {
                    getProfessor {
                      entityPersonId
                    }
                  }
                }",
                Variables = new
                {
                    input = new UpdateProfessorInput(_mapper.Map<PutProfessor>(entity))
                }
            };

            var response = await _httpClient.SendQueryAsync(request, () => new { UpdateProfessor = new { GetProfessor = new GetProfessor() } });

            if (response.Errors != null)
            {
                return default;
            }

            return response.Data.UpdateProfessor.GetProfessor.EntityPersonId;
        }
    }
}
