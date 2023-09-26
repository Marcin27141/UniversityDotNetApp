
using ApiDtoLibrary.Professors;

namespace ApiDtoLibrary.GraphQL.Professors
{
    public record AddProfessorPayload(
        string Id,
        string UserId,
        string FirstName,
        string LastName,
        string PESEL,
        string Motherland,
        DateTime Birthday,
        string ProfessorId,
        string Subject,
        DateTime FirstDayAtJob,
        int Salary
        );
}
