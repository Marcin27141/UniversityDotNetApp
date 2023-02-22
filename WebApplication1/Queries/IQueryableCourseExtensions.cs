using System.ComponentModel.DataAnnotations;
using System.Linq;
using System;
using WebApplication1.DataBase.Entities;
using WebApplication1.Services.People;
using WebApplication1.Services;

namespace WebApplication1.Queries
{
    public enum CourseOrderByOptions
    {
        [Display(Name = "sort by...")] SimpleOrder = 0,
        [Display(Name = "Name")] ByName,
        [Display(Name = "Code")] ByCode,
        [Display(Name = "ECTS")] ByECTS,
    }

    public enum CourseFilterByOptions
    {
        [Display(Name = "All")] NoFilter = 0,
        [Display(Name = "By Name...")] ByName,
        [Display(Name = "By Code...")] ByCode,
        [Display(Name = "By ECTS...")] ByECTS,
        [Display(Name = "By Exam...")] ByExam,
    }
    public static class IQueryableCourseExtensions
    {
        public static IQueryable<Course> MapEntitiesToCourses(this IQueryable<EntityCourse> courses)
        {
            return courses.Select(c => Course.FromEntityCourse(c));
        }

        public static IQueryable<EntityCourse> OrderCoursesBy(this IQueryable<EntityCourse> courses, CourseOrderByOptions orderByOptions)
        {
            switch (orderByOptions)
            {
                case CourseOrderByOptions.SimpleOrder:
                    return courses.OrderBy(c => c.EntityCourseID);
                case CourseOrderByOptions.ByName:
                    return courses.OrderBy(c => c.Name);
                case CourseOrderByOptions.ByCode:
                    return courses.OrderBy(c => c.CourseCode);
                case CourseOrderByOptions.ByECTS:
                    return courses.OrderBy(c => c.ECTS);
                default:
                    throw new ArgumentOutOfRangeException(nameof(orderByOptions), orderByOptions, null);
            }
        }

        public static IQueryable<EntityCourse> FilterCoursesBy(this IQueryable<EntityCourse> courses, CourseFilterByOptions filterByOptions, string filterValue)
        {
            switch (filterByOptions)
            {
                case CourseFilterByOptions.NoFilter:
                    return courses;
                case CourseFilterByOptions.ByName:
                    return courses.Where(c => c.Name.StartsWith(filterValue));
                case CourseFilterByOptions.ByCode:
                    return courses.Where(c => c.CourseCode.StartsWith(filterValue));
                case CourseFilterByOptions.ByECTS:
                    var canParse = int.TryParse(filterValue, out int result);
                    if (!canParse) return courses;
                    return courses.Where(c => c.ECTS >= result);
                case CourseFilterByOptions.ByExam:
                    return courses.Where(c => c.IsFinishedWithExam);
                default:
                    throw new ArgumentOutOfRangeException(nameof(filterByOptions), filterByOptions, null);
            }
        }
    }
}
