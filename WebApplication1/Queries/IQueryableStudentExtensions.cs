using ApiDtoLibrary.Students;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

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
        public static IQueryable<FullStudent> OrderStudentsBy(this IQueryable<FullStudent> students, StudentOrderByOptions orderByOptions)
        {
            switch (orderByOptions)
            {
                case StudentOrderByOptions.SimpleOrder:
                    return students.OrderBy(s => s.EntityPersonID);
                case StudentOrderByOptions.ByName:
                    return students.OrderBy(s => s.FirstName);
                case StudentOrderByOptions.BySurname:
                    return students.OrderBy(s => s.LastName);
                case StudentOrderByOptions.ByAge:
                    return students.OrderBy(s => s.Birthday);
                case StudentOrderByOptions.ByIndex:
                    return students.OrderBy(s => s.Index);
                default :
                    throw new ArgumentOutOfRangeException(nameof(orderByOptions), orderByOptions, null);
            }
        }

        public static IQueryable<FullStudent> FilterStudentsBy(this IQueryable<FullStudent> students, StudentFilterByOptions filterByOptions, string filterValue)
        {
            switch (filterByOptions)
            {
                case StudentFilterByOptions.NoFilter:
                    return students;
                case StudentFilterByOptions.BySurname:
                    return students.Where(s => s.LastName.StartsWith(filterValue));
                case StudentFilterByOptions.ByAge:
                    var canParse = int.TryParse(filterValue, out int result);
                    if (!canParse) return students;
                    return students.Where(s => DateTime.Now.Year - s.Birthday.Year > result);
                case StudentFilterByOptions.ByIndex:
                    return students.Where(s => s.Index.StartsWith(filterValue));
                default:
                    throw new ArgumentOutOfRangeException(nameof(filterByOptions), filterByOptions, null);
            }
        }
    }
}