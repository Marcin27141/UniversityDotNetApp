using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Services.People
{
    public class PersonalData
    {
        [Required]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name ="Last name")]
        public string LastName { get; set; }

        [Required]
        [RegularExpression(@"\d{11}", ErrorMessage = "Please use a 11 digit PESEL")]
        public string PESEL { get; set; }

        public DateTime Birthday { get; set; }

        public string Motherland { get; set; }
    }
}
