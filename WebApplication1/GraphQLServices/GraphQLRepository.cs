using AutoMapper;
using GraphQL;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using System;
using WebApplication1.Contracts;
using GraphQL.Client.Abstractions;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace WebApplication1.GraphQLServices
{
    public abstract class GraphQLRepository
    {
        private const string GRAPHQL_SERVER_ADDRESS = "https://localhost:7228/graphql/";
        private GraphQLHttpClient _httpClient;
        protected readonly IMapper _mapper;
        protected IAuthenticationRepository _authenticationRepository;
        

        public GraphQLRepository(IMapper mapper, IAuthenticationRepository authenticationRepository)
        {
            this._mapper = mapper;
            _authenticationRepository = authenticationRepository;
            this._httpClient = new GraphQLHttpClient(GRAPHQL_SERVER_ADDRESS, new NewtonsoftJsonSerializer());
        }

        protected async Task<T> SendGraphQLRequest<T>(GraphQLRequest request, Func<T> responseType)
        {
            var response = await _httpClient.SendQueryAsync(request, responseType);

            if (response.Errors != null)
                throw new GraphQLException(response.Errors);

            return response.Data;
        }

    }
}
