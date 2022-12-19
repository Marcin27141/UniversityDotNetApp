using System;
using System.ComponentModel.DataAnnotations;
using WebApplication1.Services.PeopleOps;

namespace WebApplication1.Services.People
{
    public class Professor
    {
        public PersonalData PersonalData { get; set; }

        [RegularExpression(@"\d{5}", ErrorMessage = "Please use a 5 digit id")]
        [Display(Name = "ID")]
        public string IdCode { get; set; }
        public string Subject { get; set; }

        [DataType(DataType.Date)]
        [Display(Name ="First day at job")]
        public DateTime FirstDayAtJob { get; set; }

        public int Salary { get; set; }

        public override string ToString() => PersonalData.ToString();

        /*public override string ToString()
        {
            return base.ToString() + $", {Subject}";
        }*/

        public DataBase.Entities.Professor ToEntityProfessor()
        {
            return new DataBase.Entities.Professor()
            {
                IdCode = this.IdCode,
                PersonalData = this.PersonalData.ToEntityPersonalData(this.IdCode,PersonType.Professor),
                Subject = this.Subject,
                FirstDayAtJob = this.FirstDayAtJob,
                Salary = this.Salary,
            };
        }

        public static Professor FromEntityProfessor(DataBase.Entities.Professor entityProfessor)
        {
            return new Professor
            {
                PersonalData = PersonalData.FromEntityPersonalData(entityProfessor.PersonalData),
                IdCode = entityProfessor.IdCode,
                Subject = entityProfessor.Subject,
                FirstDayAtJob = entityProfessor.FirstDayAtJob,
                Salary = entityProfessor.Salary
            };
        }
    }
}
