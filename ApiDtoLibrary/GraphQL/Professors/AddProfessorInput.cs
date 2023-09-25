namespace ApiDtoLibrary.GraphQL.Professors
{
    public record AddProfessorInput(
        string ApplicationUserId,
        string FirstName,
        string LastName,
        string IdCode,
        string Subject
        );
}
