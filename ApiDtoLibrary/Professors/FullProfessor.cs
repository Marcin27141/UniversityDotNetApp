using ApiDtoLibrary.Person;
using System.ComponentModel.DataAnnotations;

namespace ApiDtoLibrary.Professors
{
    public class FullProfessor : BaseProfessor
    {
        public Guid EntityPersonID { get; set; }

        public string ApplicationUserId { get; set; }

        public PersonStatus PersonStatus { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "First day at job")]
        public DateTime FirstDayAtJob { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Salary must be greater than 0")]
        public int Salary { get; set; }
    }
}
