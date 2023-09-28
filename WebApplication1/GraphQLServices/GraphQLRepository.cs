using AutoMapper;
using GraphQL;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using System;
using WebApplication1.Contracts;
using GraphQL.Client.Abstractions;
using System.Threading.Tasks;
using WebApplication1.GraphQLServices.GraphQLDtos;
using System.Reactive.Linq;
using MyExtensions = GraphQL.Client.Abstractions.GraphQLClientExtensions;

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
            var clientOptions = new GraphQLHttpClientOptions { 
                EndPoint = new Uri(GRAPHQL_SERVER_ADDRESS),
                UseWebSocketForQueriesAndMutations = true,
            };
            this._httpClient = new GraphQLHttpClient(clientOptions, new NewtonsoftJsonSerializer());
        }

        protected async Task<T> SendGraphQLRequest<T>(GraphQLRequest request, Func<T> responseType)
        {
            var response = await _httpClient.SendQueryAsync(request, responseType);

            if (response.Errors != null)
                throw new GraphQLException(response.Errors);

            return response.Data;
        }

        public class MySubscriptionResponse
        {
            public GraphQLCourseDto OnCourseProfessorAssigment { get; set; }
        }

        protected Task SendGraphQLSubscription<T>(GraphQLRequest request, Action<T> onEventReceived)
        {
            
            var stream = MyExtensions.CreateSubscriptionStream(_httpClient, request, () => new { OnCourseProfessorAssignment = new GraphQLCourseDto() });
            stream.Subscribe(
                response => {
                    Console.WriteLine(response.Data.OnCourseProfessorAssignment.Name);
                    //onEventReceived(response.Data.On);
                });
            return Task.CompletedTask;
        }
    }
}
