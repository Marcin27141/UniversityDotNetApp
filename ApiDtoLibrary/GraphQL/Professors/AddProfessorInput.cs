#nullable enable

namespace ApiDtoLibrary.GraphQL.Professors
{
    public record AddProfessorInput(
        string ApplicationUserId,
        string FirstName,
        string LastName,
        string? PESEL,
        string? Motherland,
        DateTime? Birthday,
        string IdCode,
        string? Subject,
        DateTime? FirstDayAtJob,
        int Salary
        );
}
