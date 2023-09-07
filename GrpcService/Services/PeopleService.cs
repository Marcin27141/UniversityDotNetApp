using ApiDtoLibrary.Person;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using GrpcService.Database;
using GrpcService.Models;
using GrpcService.Protos;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using static Google.Rpc.Context.AttributeContext.Types;

namespace GrpcService.Services
{
    public class PeopleService : PeopleServer.PeopleServerBase
    {
        private readonly GrpcDbContext _dbContext;

        public PeopleService(GrpcDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public override async Task<CreatePersonResponse> CreatePerson(CreatePersonRequest request, ServerCallContext context)
        {
            var nullChecklist = new List<String>() { request.ApplicationUserId.Value, request.FirstName, request.LastName };
            VerifyStringsNullabilityRequirements(nullChecklist);
            var guidChecklist = new List<String>() { request.ApplicationUserId.Value };
            VerifyGuidsValidity(guidChecklist);


            var person = new PersonalData
            {
                ApplicationUserId = Guid.Parse(request.ApplicationUserId.Value),
                FirstName = request.FirstName,
                LastName = request.LastName,
                PESEL = request.PESEL,
                Birthday = request.Birthday.ToDateTime(),
                Motherland = request.Motherland,
                PersonStatus = (PersonStatus)(int)request.PersonStatus
            };

            await _dbContext.AddAsync(person);
            await _dbContext.SaveChangesAsync();

            return await Task.FromResult(new CreatePersonResponse
            {
                PersonId = new GrpcUUID { Value = person.PersonId.ToString() }
            });
        }

        private void VerifyStringsNullabilityRequirements(List<String> strings)
        {
            if (CheckIfAnyIsNullOrEmpty(strings))
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Nulls or empty strings were passed for non-nullable properties"));
            }
        }

        private bool CheckIfAnyIsNullOrEmpty(List<String> strings)
        {
            return strings.Any(s => s.IsNullOrEmpty());
        }

        private void VerifyGuidsValidity(List<String> strings)
        {
            if (!CheckIfAllStringAreGuid(strings))
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Guids were expected for UUID properties"));
            }
        }

        private bool CheckIfAllStringAreGuid(List<String> strings)
        {
            return strings.All(s => Guid.TryParse(s, out Guid _));
        }

        public override async Task<ReadPersonResponse> ReadPerson(ReadPersonRequest request, ServerCallContext context)
        {
            var personIdString = request.PersonId.Value;
            VerifyGuidsValidity(new List<String>() { personIdString });


            var person = await _dbContext.People.FindAsync(Guid.Parse(personIdString));

            if (person != null)
                return await Task.FromResult(new ReadPersonResponse
                {
                    PersonId = new GrpcUUID { Value = personIdString },
                    ApplicationUserId = new GrpcUUID { Value = person.ApplicationUserId.ToString() },
                    FirstName = person.FirstName,
                    LastName = person.LastName,
                    PESEL = person.PESEL,
                    Birthday = person.Birthday?.ToUniversalTime().ToTimestamp(),
                    Motherland = person.Motherland,
                    PersonStatus = (GrpcPersonStatus)(int)person.PersonStatus
                });
            else
                throw new RpcException(new Status(StatusCode.NotFound, $"No person with id {personIdString} was found"));
        }

        public override async Task<ReadAllPeopleResponse> ReadPeople(ReadAllPeopleRequest request, ServerCallContext context)
        {
            var response = new ReadAllPeopleResponse();

            var people = await _dbContext.People.ToListAsync();
            people.ForEach(person => response.People.Add(new ReadPersonResponse
            {
                PersonId = new GrpcUUID { Value = person.PersonId.ToString() },
                ApplicationUserId = new GrpcUUID { Value = person.ApplicationUserId.ToString() },
                FirstName = person.FirstName,
                LastName = person.LastName,
                PESEL = person.PESEL,
                Birthday = person.Birthday?.ToUniversalTime().ToTimestamp(),
                Motherland = person.Motherland,
                PersonStatus = (GrpcPersonStatus)(int)person.PersonStatus
            }));

            return await Task.FromResult(response);
        }

        public override async Task<UpdatePersonResponse> UpdatePerson(UpdatePersonRequest request, ServerCallContext context)
        {
            var personIdString = request.PersonId.Value;
            VerifyGuidsValidity(new List<String>() { personIdString });

            var person = await _dbContext.People.FindAsync(Guid.Parse(personIdString));

            if (person != null)
            {
                UpdatePersonProperties(request, person);
                await _dbContext.SaveChangesAsync();
                return await Task.FromResult(new UpdatePersonResponse()
                {
                    PersonId = new GrpcUUID
                    {
                        Value = personIdString
                    }
                });
            }
            else
                throw new RpcException(new Status(StatusCode.NotFound, $"No person with id {personIdString} was found"));
        }

        private void UpdatePersonProperties(UpdatePersonRequest request, PersonalData person)
        {
            person.FirstName = request.FirstName;
            person.LastName = request.LastName;
            person.PESEL = request.PESEL;
            person.Birthday = request.Birthday?.ToDateTime();
            person.Motherland = request.Motherland;
        }

        public override async Task<DeletePersonResponse> DeletePerson(DeletePersonRequest request, ServerCallContext context)
        {
            var personIdString = request.PersonId.Value;
            VerifyGuidsValidity(new List<String>() { personIdString });

            var person = await _dbContext.People.FindAsync(Guid.Parse(personIdString));

            if (person != null)
            {
                _dbContext.Remove(person);
                await _dbContext.SaveChangesAsync();
                return await Task.FromResult(new DeletePersonResponse()
                {
                    PersonId = new GrpcUUID
                    {
                        Value = personIdString
                    }
                });
            }
            else
                throw new RpcException(new Status(StatusCode.NotFound, $"No person with id {personIdString} was found"));
        }
    }
}
