using ApiDtoLibrary.Courses;
using ApiDtoLibrary.Person;
using System.ComponentModel.DataAnnotations;

namespace ApiDtoLibrary.Students
{
    public class GetStudent : BaseGetStudent
    {
        public string ApplicationUserId { get; set; }

        public PersonStatus PersonStatus { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "First day of studying")]
        public DateTime BeginningOfStudying { get; set; }

        public List<BaseGetCourse> Courses { get; set; }
    }
}
