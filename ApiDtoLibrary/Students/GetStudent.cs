using System.ComponentModel.DataAnnotations;

namespace ApiDtoLibrary.Students
{
    public class GetStudent : BaseStudent
    {
        [Required]
        [Display(Name = "Id")]
        public int EntityPersonID { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "First day of studying")]
        public DateTime BeginningOfStudying { get; set; }
    }
}
