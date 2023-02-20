using System;
using System.ComponentModel.DataAnnotations;
using WebApplication1.DataBase.Entities;
using WebApplication1.Services.PeopleOps;

namespace WebApplication1.Services.People
{
    public class PersonalData
    {
        public ApplicationUser ApplicationUser { get; set; }

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

        public override string ToString() => $"{FirstName} {LastName}";

        public EntityPersonalData ToEntityPersonalData(string specificId, PersonType personType)
        {
            return new EntityPersonalData()
            {
                ApplicationUser = this.ApplicationUser,
                SpecificId = specificId,
                PersonTypeID = (int)personType,
                FirstName = this.FirstName,
                LastName = this.LastName,
                PESEL = this.PESEL,
                Birthday = this.Birthday,
                Motherland = this.Motherland,
            };
        }

        public static PersonalData FromEntityPersonalData(EntityPersonalData entityPersonalData)
        {
            return new PersonalData
            {
                ApplicationUser = entityPersonalData.ApplicationUser,
                FirstName = entityPersonalData.FirstName,
                LastName = entityPersonalData.LastName,
                PESEL = entityPersonalData.PESEL,
                Birthday = entityPersonalData.Birthday,
                Motherland = entityPersonalData.Motherland,
            };
        }
    }
}
