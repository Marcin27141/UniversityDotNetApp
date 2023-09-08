namespace ApiDtoLibrary.GraphQL.Students
{
    public record AddStudentInput(
        Guid ApplicationUserId,
        string FirstName,
        string LastName,
        string Index
        );
}
