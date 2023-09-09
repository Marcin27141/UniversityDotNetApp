using ApiDtoLibrary.Person;
using AutoMapper;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using GrpcService.Database;
using GrpcService.Models;
using GrpcService.Protos;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using static GrpcService.Services.GrpcModelHelper;

namespace GrpcService.Services
{
    public class PeopleService : PeopleServer.PeopleServerBase
    {
        private readonly GrpcDbContext _dbContext;
        private readonly IMapper _mapper;

        public PeopleService(GrpcDbContext dbContext, IMapper mapper)
        {
            this._dbContext = dbContext;
            this._mapper = mapper;
        }

        public override async Task<CreatePersonResponse> CreatePerson(CreatePersonRequest request, ServerCallContext context)
        {
            var nullChecklist = new List<String>() { request.ApplicationUserId, request.FirstName, request.LastName };
            VerifyStringsNullabilityRequirements(nullChecklist);
            var guidChecklist = new List<String>() { request.ApplicationUserId };
            VerifyGuidsValidity(guidChecklist);


            var person = _mapper.Map<Person>(request);
            await _dbContext.AddAsync(person);
            await _dbContext.SaveChangesAsync();

            return await Task.FromResult(new CreatePersonResponse
            {
                PersonId = person.PersonId.ToString()
            });
        }

        public override async Task<ReadPersonResponse> ReadPerson(ReadPersonRequest request, ServerCallContext context)
        {
            var personIdString = request.PersonId;
            VerifyGuidsValidity(new List<string>() { personIdString });


            var person = await _dbContext.People.FindAsync(Guid.Parse(personIdString));

            if (person != null)
                return await Task.FromResult(_mapper.Map<ReadPersonResponse>(person));
            else
                throw new RpcException(new Status(StatusCode.NotFound, $"No person with id {personIdString} was found"));
        }

        public override async Task<ReadAllPeopleResponse> ReadPeople(ReadAllPeopleRequest request, ServerCallContext context)
        {
            var response = new ReadAllPeopleResponse();

            var people = await _dbContext.People.ToListAsync();
            people.ForEach(person => response.People.Add(_mapper.Map<ReadPersonResponse>(person)));

            return await Task.FromResult(response);
        }

        public override async Task<UpdatePersonResponse> UpdatePerson(UpdatePersonRequest request, ServerCallContext context)
        {
            var nullChecklist = new List<String>() { request.FirstName, request.LastName };
            VerifyStringsNullabilityRequirements(nullChecklist);
            var personIdString = request.PersonId;
            VerifyGuidsValidity(new List<String>() { personIdString });

            var person = await _dbContext.People.FindAsync(Guid.Parse(personIdString));

            if (person != null)
            {
                UpdatePersonProperties(request, person);
                await _dbContext.SaveChangesAsync();
                return await Task.FromResult(new UpdatePersonResponse()
                {
                    PersonId = personIdString
                });
            }
            else
                throw new RpcException(new Status(StatusCode.NotFound, $"No person with id {personIdString} was found"));
        }

        private void UpdatePersonProperties(UpdatePersonRequest request, Person person)
        {
            person.FirstName = request.FirstName;
            person.LastName = request.LastName;
            person.PESEL = request.PESEL;
            person.Birthday = request.Birthday.ToDateTime();
            person.Motherland = request.Motherland;
        }

        public override async Task<DeletePersonResponse> DeletePerson(DeletePersonRequest request, ServerCallContext context)
        {
            var personIdString = request.PersonId;
            VerifyGuidsValidity(new List<String>() { personIdString });

            var person = await _dbContext.People.FindAsync(Guid.Parse(personIdString));

            if (person != null)
            {
                _dbContext.Remove(person);
                await _dbContext.SaveChangesAsync();
                return await Task.FromResult(new DeletePersonResponse()
                {
                    PersonId = personIdString
                });
            }
            else
                throw new RpcException(new Status(StatusCode.NotFound, $"No person with id {personIdString} was found"));
        }
    }
}
