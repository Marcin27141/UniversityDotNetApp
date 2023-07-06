using System.ComponentModel.DataAnnotations;

namespace ApiDtoLibrary.Professors
{
    public class PutProfessor : BaseProfessor
    {
        [Required]
        [Display(Name = "Id")]
        public int EntityPersonID { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "First day at job")]
        public DateTime FirstDayAtJob { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Salary must be greater than 0")]
        public int Salary { get; set; }
    }
}
