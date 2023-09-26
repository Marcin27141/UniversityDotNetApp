#nullable enable

namespace ApiDtoLibrary.GraphQL.Professors
{
    public record UpdateProfessorInput(
        string Id,
        string FirstName,
        string LastName,
        string? PESEL,
        string? Motherland,
        DateTime? Birthday,
        string? Subject,
        DateTime? FirstDayAtJob,
        int Salary
        );
}
