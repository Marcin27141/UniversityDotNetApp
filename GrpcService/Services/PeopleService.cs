using ApiDtoLibrary.Person;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using GrpcService.Database;
using GrpcService.Models;
using GrpcService.Protos;
using Microsoft.IdentityModel.Tokens;

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
            VerifyStringsNullabilityRequirements(request);
            VerifyGuidsValidity(request);


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

        private void VerifyStringsNullabilityRequirements(CreatePersonRequest request)
        {
            var checklist = new List<String>() { request.ApplicationUserId.Value, request.FirstName, request.LastName };
            if (CheckIfAnyIsNullOrEmpty(checklist))
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Nulls or empty strings were passed for non-nullable properties"));
            }
        }

        private bool CheckIfAnyIsNullOrEmpty(List<String> strings)
        {
            return strings.Any(s => s.IsNullOrEmpty());
        }

        private void VerifyGuidsValidity(CreatePersonRequest request)
        {
            var checklist = new List<String>() { request.ApplicationUserId.Value };
            if (!CheckIfAllStringAreGuid(checklist))
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Guids were expected for UUID properties"));
            }
        }

        private bool CheckIfAllStringAreGuid(List<String> strings)
        {
            return strings.All(s => Guid.TryParse(s, out Guid _));
        }
    }
}
