using ApiDtoLibrary.Person;
using System.ComponentModel.DataAnnotations;

namespace ApiDtoLibrary.Professors
{
    public class GetProfessor : BaseGetProfessor
    {
        public string ApplicationUserId { get; set; }

        public PersonStatus PersonStatus { get; set; }

        public string Subject { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "First day at job")]
        public DateTime FirstDayAtJob { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Salary must be greater than 0")]
        public int Salary { get; set; }
    }
}
