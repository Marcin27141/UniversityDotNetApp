namespace ApiDtoLibrary.GraphQL.Professors
{
    public record AddProfessorInput(
        Guid ApplicationUserId,
        string FirstName,
        string LastName,
        string IdCode,
        string Subject
        );
}
