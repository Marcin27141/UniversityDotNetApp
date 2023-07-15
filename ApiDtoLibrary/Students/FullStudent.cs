using System.ComponentModel.DataAnnotations;
using ApiDtoLibrary.Courses;
using ApiDtoLibrary.Person;

namespace ApiDtoLibrary.Students
{
    public class FullStudent : BaseStudent
    {
        public Guid EntityPersonId { get; set; }

        public string ApplicationUserId { get; set; }

        public PersonStatus PersonStatus { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "First day of studying")]
        public DateTime BeginningOfStudying { get; set; }

        public List<FullCourse> Courses { get; set; }
    }
}
