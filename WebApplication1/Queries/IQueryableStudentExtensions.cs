using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using WebApplication1.DataBase;
using WebApplication1.DataBase.Entities;
using WebApplication1.Services.People;

namespace WebApplication1.Queries
{
    public enum StudentOrderByOptions
    {
        [Display(Name = "sort by...")] SimpleOrder = 0,
        [Display(Name = "Name")] ByName,
        [Display(Name = "Surname")] BySurname,
        [Display(Name = "Age")] ByAge,
        [Display(Name = "Index")] ByIndex
    }

    public enum StudentFilterByOptions
    {
        [Display(Name = "All")] NoFilter = 0,
        [Display(Name = "By Surname...")] BySurname,
        [Display(Name = "By Age...")] ByAge,
        [Display(Name = "By Index...")] ByIndex
    }
    public static class IQueryableStudentExtensions
    {
        public static IQueryable<Student> MapEntitiesToStudents(this IQueryable<EntityStudent> students)
        {
            return students.Select(s => Student.FromEntityStudent(s));
        }

        public static IQueryable<EntityStudent> OrderStudentsBy(this IQueryable<EntityStudent> students, StudentOrderByOptions orderByOptions)
        {
            switch (orderByOptions)
            {
                case StudentOrderByOptions.SimpleOrder:
                    return students.OrderByDescending(s => s.EntityStudentID);
                case StudentOrderByOptions.ByName:
                    return students.OrderByDescending(s => s.PersonalData.FirstName);
                case StudentOrderByOptions.BySurname:
                    return students.OrderByDescending(s => s.PersonalData.LastName);
                case StudentOrderByOptions.ByAge:
                    return students.OrderByDescending(s => s.PersonalData.Birthday);
                case StudentOrderByOptions.ByIndex:
                    return students.OrderByDescending(s => s.StudentIndex);
                default :
                    throw new ArgumentOutOfRangeException(nameof(orderByOptions), orderByOptions, null);
            }
        }

        public static IQueryable<EntityStudent> FilterStudentsBy(this IQueryable<EntityStudent> students, StudentFilterByOptions filterByOptions, string filterValue)
        {
            switch (filterByOptions)
            {
                case StudentFilterByOptions.NoFilter:
                    return students;
                case StudentFilterByOptions.BySurname:
                    return students.Where(s => s.PersonalData.LastName.StartsWith(filterValue));
                case StudentFilterByOptions.ByAge:
                    var filterAge = int.Parse(filterValue);
                    return students.Where(s => DateTime.Now.Year - s.PersonalData.Birthday.Year > filterAge);
                case StudentFilterByOptions.ByIndex:
                    return students.Where(s => s.StudentIndex.StartsWith(filterValue));
                default:
                    throw new ArgumentOutOfRangeException(nameof(filterByOptions), filterByOptions, null);
            }
        }
    }
}