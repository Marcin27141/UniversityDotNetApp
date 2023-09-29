using ApiDtoLibrary.GraphQL.Professors;
using ApiDtoLibrary.Professors;
using AutoMapper;
using GraphQL;
using System;
using WebApplication1.Services.People;

namespace WebApplication1.GraphQLServices.QueryGenerators
{
    public class PersonGraphQLQueryGenerator : IPersonGraphQLQueryGenerator
    {
        private readonly IMapper _mapper;

        public PersonGraphQLQueryGenerator(IMapper mapper)
        {
            this._mapper = mapper;
        }

        public GraphQLRequest GetQueryForDeleteById(Guid personId)
        {
            return new GraphQLRequest
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
        }

        public GraphQLRequest GetQueryForGetAll()
        {
            return new GraphQLRequest
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
                }"
                ,
            };
        }

        public GraphQLRequest GetQueryForGetById(Guid id)
        {
            return new GraphQLRequest
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
        }

        public GraphQLRequest GetQueryForGetNotifications(string recipientId)
        {
            return new GraphQLRequest
            {
                Query = @"
                query GetPersonById($id: String) {
                  personById(id: $id) {
                    Notifications {
                        Id
                        RecipientId
                        Title
                        Body
                        IsNew
                    }
                  }
                }",
                Variables = new
                {
                    id = recipientId
                }
            };
        }
    }



    public interface IPersonGraphQLQueryGenerator
    {
        GraphQLRequest GetQueryForGetAll();
        GraphQLRequest GetQueryForDeleteById(Guid personId);
        GraphQLRequest GetQueryForGetById(Guid id);
        GraphQLRequest GetQueryForGetNotifications(string recipientId);
    }
}
