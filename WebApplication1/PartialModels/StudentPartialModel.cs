using WebApplication1.Services.People;

namespace WebApplication1.PartialModels
{
    public class StudentPartialModel
    {
        public Student Student { get; set; }
        public bool ShowCourses { get; set; }
    }
}
