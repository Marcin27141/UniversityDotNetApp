namespace GrpcService.Models
{
    public class Course
    {
        public Guid CourseID { get; set; }

        public Professor? Professor { get; set; }
        public Guid? ProfessorId { get; set; }

        public IList<Grade> Grades { get; set; } = new List<Grade>();
    }
}
