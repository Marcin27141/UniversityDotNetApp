using System;
using System.ComponentModel.DataAnnotations;
using WebApplication1.Services.PeopleOps;

namespace WebApplication1.Services.People
{
    public class PersonalData
    {
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Display(Name ="Last name")]
        public string LastName { get; set; }

        public string PESEL { get; set; }

        public DateTime Birthday { get; set; }

        public string Motherland { get; set; }

        public override string ToString() => $"{FirstName} {LastName}";

        public DataBase.Entities.PersonalData ToEntityPersonalData(string specificId, PersonType personType)
        {
            return new DataBase.Entities.PersonalData()
            {
                SpecificId = specificId,
                PersonTypeID = (int)personType,
                FirstName = this.FirstName,
                LastName = this.LastName,
                PESEL = this.PESEL,
                Birthday = this.Birthday,
                Motherland = this.Motherland,
            };
        }

        public static PersonalData FromEntityPersonalData(DataBase.Entities.PersonalData entityPersonalData)
        {
            return new PersonalData
            {
                FirstName = entityPersonalData.FirstName,
                LastName = entityPersonalData.LastName,
                PESEL = entityPersonalData.PESEL,
                Birthday = entityPersonalData.Birthday,
                Motherland = entityPersonalData.Motherland,
            };
        }
    }
}
