using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using WebApplication1.Contracts;
using WebApplication1.Services.People;
using GraphQL;
using System.Linq;
using WebApplication1.Extensions;
using System.Security.Claims;
using ApiDtoLibrary.Professors;
using WebApplication1.GraphQLServices.GraphQLDtos;
using WebApplication1.GraphQLServices.QueryGenerators;
using static Grpc.Core.Metadata;
using ApiDtoLibrary.GraphQL.Professors;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.GraphQLServices
{
    public class ProfessorsRepository : GraphQLRepository, IProfessorsRepository
    {
        private readonly IProfessorGraphQLQueryGenerator _professorQueryGenerator;

        public ProfessorsRepository(IMapper mapper, IAuthenticationRepository authenticationRepository, IProfessorGraphQLQueryGenerator professorQueryGenerator) : base(mapper, authenticationRepository)
        {
            this._professorQueryGenerator = professorQueryGenerator;
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
            GraphQLRequest request = _professorQueryGenerator.GetQueryForIdIsOccupied(idCode);
            var response = await SendGraphQLRequest(request, () => new { Professors = new List<GraphQLProfessorDto>() });
            return response.Professors.Count > 0;
        }

        public async Task<Professor> GetAsync(Guid id)
        {
            GraphQLRequest request = _professorQueryGenerator.GetQueryForGetById(id);
            var response = await SendGraphQLRequest(request, () => new { ProfessorById = new GraphQLProfessorDto() });
            return _mapper.Map<Professor>(response.ProfessorById);
        }

        public async Task<List<Professor>> GetAllAsync()
        {
            GraphQLRequest request = _professorQueryGenerator.GetQueryForGetAll();
            var response = await SendGraphQLRequest(request, () => new { Professors = new List<GraphQLProfessorDto>() });
            return response.Professors.Select(_mapper.Map<Professor>).ToList();
        }

        public async Task<Professor> GetByUserAsync(string userId)
        {
            GraphQLRequest request = _professorQueryGenerator.GetQueryForGetByUser(userId);
            var response = await SendGraphQLRequest(request, () => new { Professors = new List<GraphQLProfessorDto>() });

            if (response.Professors.Count == 0)
                return default;
            var professor = response.Professors.SingleOrDefault();

            return _mapper.Map<Professor>(professor);
        }

        public async Task AddClaimAfterPostAsync(string userId, Claim claim)
        {
            await _authenticationRepository.AddClaimAsync(userId, claim);
        }

        public async Task<GetProfessor> AddAsync(Professor entity)
        {
            GraphQLRequest request = _professorQueryGenerator.GetQueryForAdd(entity);
            var response = await SendGraphQLRequest(request, () => new { AddProfessor = new { GetProfessor = new GetProfessor() } });

            var getProfessor = response.AddProfessor.GetProfessor;
            await _authenticationRepository.AddEntityPersonIdClaimAsync(getProfessor.ApplicationUserId, getProfessor.EntityPersonId.ToString());

            return getProfessor;
        }

        public async Task<Guid> UpdateAsync(Professor entity)
        {
            GraphQLRequest request = _professorQueryGenerator.GetQueryForUpdate(entity);
            var response = await SendGraphQLRequest(request, () => new { UpdateProfessor = new { GetProfessor = new GetProfessor() } });
            return response.UpdateProfessor.GetProfessor.EntityPersonId;
        }

        public class SubscriptionResponse
        {
            public GraphQLCourseDto OnCourseProfessorAssigment { get; set; }
    }

    public async Task SubscribeForCourseAssignments()
        {
            GraphQLRequest request = new GraphQLRequest
            {
                Query = @"
                subscription {
                  onCourseProfessorAssignment
                  {
                    Id
                    CourseCode
                    Name
                    Professor {
                        Id
                    }
                  }
                }",
            };
            await SendGraphQLSubscription<SubscriptionResponse>(request, response => Console.WriteLine(response.OnCourseProfessorAssigment));
        }
    }
}
