using System.ComponentModel.DataAnnotations;

namespace ApiDtoLibrary.Students
{
    public class PostStudent : BaseStudent
    {
        [DataType(DataType.Date)]
        [Display(Name = "First day of studying")]
        public DateTime BeginningOfStudying { get; set; }
    }
}
