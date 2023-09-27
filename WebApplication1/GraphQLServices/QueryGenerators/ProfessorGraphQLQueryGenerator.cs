using ApiDtoLibrary.GraphQL.Professors;
using ApiDtoLibrary.Professors;
using AutoMapper;
using GraphQL;
using System;
using WebApplication1.Services.People;

namespace WebApplication1.GraphQLServices.QueryGenerators
{
    public class ProfessorGraphQLQueryGenerator : IProfessorGraphQLQueryGenerator
    {
        private readonly IMapper _mapper;

        public ProfessorGraphQLQueryGenerator(IMapper mapper)
        {
            this._mapper = mapper;
        }

        public GraphQLRequest GetQueryForAdd(Professor entity)
        {
            return new GraphQLRequest
            {
                Query = @"
                mutation AddProfessorMutation($input: AddProfessorInput) {
                  addProfessor (input: $input)
                  {
                    getProfessor {
                        entityPersonId
                        applicationUserId
                        firstName
                        lastName
                        pesel
                        motherland
                        birthday
                        idCode
                        subject
                        firstDayAtJob
                        salary
                    }
                  }
                }",
                Variables = new
                {
                    input = new AddProfessorInput(_mapper.Map<PostProfessor>(entity))
                }
            };
        }

        public GraphQLRequest GetQueryForGetAll()
        {
            return new GraphQLRequest
            {
                Query = @"
                query GetAllProfessors {
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
        }

        public GraphQLRequest GetQueryForGetById(Guid id)
        {
            return new GraphQLRequest
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
        }

        public GraphQLRequest GetQueryForGetByUser(string userId)
        {
            return new GraphQLRequest
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
        }

        public GraphQLRequest GetQueryForIdIsOccupied(string idCode)
        {
            return new GraphQLRequest
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
        }

        public GraphQLRequest GetQueryForUpdate(Professor entity)
        {
            return new GraphQLRequest
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
        }
    }

    public interface IProfessorGraphQLQueryGenerator
    {
        GraphQLRequest GetQueryForIdIsOccupied(string idCode);
        GraphQLRequest GetQueryForGetById(Guid id);
        GraphQLRequest GetQueryForGetAll();
        GraphQLRequest GetQueryForGetByUser(string userId);
        GraphQLRequest GetQueryForAdd(Professor entity);
        GraphQLRequest GetQueryForUpdate(Professor entity);
    }
}
