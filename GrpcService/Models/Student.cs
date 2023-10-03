namespace GrpcService.Models
{
    public class Student : Person
    {
        public string Index { get; set; } = String.Empty;
        public IList<Grade> Grades { get; set; } = new List<Grade>();
    }
}
