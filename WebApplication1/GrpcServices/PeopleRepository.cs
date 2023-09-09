using ApiDtoLibrary.Person;
using AutoMapper;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApplication1.ApiServices.BaseRepositories;
using WebApplication1.Contracts;
using WebApplication1.Services.People;
using Grpc.Net.Client;
using Grpc.Core;
using GrpcService.Services;
using GrpcService.Protos;

namespace WebApplication1.GrpcServices
{
    public class PeopleRepository : IPeopleRepository
    {
        private readonly IMapper _mapper;
        private readonly IAuthenticationRepository _authenticationRepository;
        private const string SERVER_ADDRESS = "https://localhost:7024";

        public PeopleRepository(IMapper mapper, IAuthenticationRepository authenticationRepository)
        {
            this._mapper = mapper;
            _authenticationRepository = authenticationRepository;
        }

        private PeopleServer.PeopleServerClient GetPeopleServerClient(GrpcChannel channel)
        {
            return new PeopleServer.PeopleServerClient(channel);
        }

        public List<Person> GetAllPersonalData()
        {
            using var channel = GrpcChannel.ForAddress(SERVER_ADDRESS);
            var client = GetPeopleServerClient(channel);

            var request = new ReadAllPeopleRequest();
            var response = client.ReadPeople(request);
            return _mapper.Map<List<Person>>(response.People.ToList());
        }

        public async Task DeleteAsync(Guid id)
        {
            using var channel = GrpcChannel.ForAddress(SERVER_ADDRESS);
            var client = GetPeopleServerClient(channel);

            try
            {
                var request = new DeletePersonRequest() { PersonId = id.ToString() };
                await client.DeletePersonAsync(request);
            }
            catch (RpcException)
            {
            }
    
        }

        public async Task<Person> GetPerson(Guid id)
        {
            using var channel = GrpcChannel.ForAddress(SERVER_ADDRESS);
            var client = GetPeopleServerClient(channel);

            var request = new ReadPersonRequest
            {
                PersonId = id.ToString()
            };

            try
            {
                var response = await client.ReadPersonAsync(request);
                return _mapper.Map<Person>(response);
            }
            catch (RpcException)
            {
                return null;
            }
        }
    }
}
