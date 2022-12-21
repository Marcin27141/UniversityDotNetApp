using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebApplication1.Services.PeopleOps;
using WebApplication1.Services.ProfessorOps;

namespace WebApplication1.Services.People
{
    public class Professor /*: IValidatableObject*/
    {
        public PersonalData PersonalData { get; set; }

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

        public override string ToString() => PersonalData.ToString();

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
