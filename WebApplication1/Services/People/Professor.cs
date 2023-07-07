using System;
using System.ComponentModel.DataAnnotations;
using WebApplication1.Services.PeopleOps;

namespace WebApplication1.Services.People
{
    public class Professor : Person
    {
        public static readonly int PROFESSOR_ENTITY_CLASS_ID = 2;


        [Required]
        [RegularExpression(@"\d{5}", ErrorMessage = "Please use a 5 digit id")]
        [Display(Name = "ID")]
        public string IdCode { get; set; }
        public string Subject { get; set; }

        [DataType(DataType.Date)]
        [Display(Name ="First day at job")]
        public DateTime FirstDayAtJob { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Salary must be greater than 0")]
        public int Salary { get; set; }

        public override string Key => IdCode;

        public override int EntityClassId => PROFESSOR_ENTITY_CLASS_ID;

        public override string ToString() => PersonalData.ToString();
    }
}
