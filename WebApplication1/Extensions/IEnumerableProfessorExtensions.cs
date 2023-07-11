using System.ComponentModel.DataAnnotations;
using System.Linq;
using System;
using System.Collections.Generic;
using WebApplication1.Services.People;

namespace WebApplication1.Extensions
{
    public enum ProfessorOrderByOptions
    {
        [Display(Name = "sort by...")] SimpleOrder = 0,
        [Display(Name = "Name")] ByName,
        [Display(Name = "Surname")] BySurname,
        [Display(Name = "Age")] ByAge,
        [Display(Name = "Id code")] ByIdCode,
        [Display(Name = "Salary")] BySalary
    }

    public enum ProfessorFilterByOptions
    {
        [Display(Name = "All")] NoFilter = 0,
        [Display(Name = "By Surname...")] BySurname,
        [Display(Name = "By Age...")] ByAge,
        [Display(Name = "By Id Code...")] ByIdCode,
        [Display(Name = "By Salary...")] BySalary
    }
    public static class IEnumerableProfessorExtensions
    {
        public static IEnumerable<Professor> OrderProfessorsBy(this IEnumerable<Professor> professors, ProfessorOrderByOptions orderByOptions)
        {
            switch (orderByOptions)
            {
                case ProfessorOrderByOptions.SimpleOrder:
                    return professors.OrderBy(p => p.EntityPersonID);
                case ProfessorOrderByOptions.ByName:
                    return professors.OrderBy(p => p.PersonalData.FirstName);
                case ProfessorOrderByOptions.BySurname:
                    return professors.OrderBy(p => p.PersonalData.LastName);
                case ProfessorOrderByOptions.ByAge:
                    return professors.OrderBy(p => p.PersonalData.Birthday);
                case ProfessorOrderByOptions.ByIdCode:
                    return professors.OrderBy(p => p.IdCode);
                case ProfessorOrderByOptions.BySalary:
                    return professors.OrderBy(p => p.Salary);
                default:
                    throw new ArgumentOutOfRangeException(nameof(orderByOptions), orderByOptions, null);
            }
        }

        public static IEnumerable<Professor> FilterProfessorsBy(this IEnumerable<Professor> professors, ProfessorFilterByOptions filterByOptions, string filterValue)
        {
            bool canParse;

            switch (filterByOptions)
            {
                case ProfessorFilterByOptions.NoFilter:
                    return professors;
                case ProfessorFilterByOptions.BySurname:
                    return professors.Where(p => p.PersonalData.LastName.StartsWith(filterValue));
                case ProfessorFilterByOptions.ByAge:
                    canParse = int.TryParse(filterValue, out int parseAge);
                    if (!canParse) return professors;
                    return professors.Where(p => DateTime.Now.Year - p.PersonalData.Birthday.Year > parseAge);
                case ProfessorFilterByOptions.ByIdCode:
                    return professors.Where(p => p.IdCode.StartsWith(filterValue));
                case ProfessorFilterByOptions.BySalary:
                    canParse = int.TryParse(filterValue, out int parseSalary);
                    if (!canParse) return professors;
                    return professors.Where(p => p.Salary >= parseSalary);
                default:
                    throw new ArgumentOutOfRangeException(nameof(filterByOptions), filterByOptions, null);
            }
        }
    }
}
