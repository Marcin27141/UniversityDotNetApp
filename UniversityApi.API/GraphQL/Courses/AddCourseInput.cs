namespace UniversityApi.API.GraphQL.Courses
{
    public record AddCourseInput(
        string CourseCode,
        string Name,
        int ECTS,
        bool IsFinishedWithExam,
        Guid ProfessorId
        );
}
