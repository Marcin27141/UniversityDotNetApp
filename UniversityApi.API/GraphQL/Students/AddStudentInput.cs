namespace UniversityApi.API.GraphQL.Students
{
    public record AddStudentInput(
        Guid ApplicationUserId,
        string FirstName,
        string LastName,
        string Index
        );
}
