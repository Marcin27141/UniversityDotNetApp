using ApiDtoLibrary.Courses;
using ApiDtoLibrary.Person;
using System.ComponentModel.DataAnnotations;

namespace ApiDtoLibrary.Students
{
    public class GetStudent : BaseStudent
    {
        public Guid EntityPersonID { get; set; }

        public string ApplicationUserId { get; set; }

        public PersonStatus PersonStatus { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "First day of studying")]
        public DateTime BeginningOfStudying { get; set; }

        public List<GetCourse> Courses { get; set; }
    }
}
