
using ApiDtoLibrary.Professors;

namespace ApiDtoLibrary.GraphQL.Professors
{
    public record AddProfessorPayload(
        string Id,
        string UserId,
        string FirstName,
        string LastName,
        string ProfessorId,
        string Subject
        );
}
