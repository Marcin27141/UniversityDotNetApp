using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using UniversityApi.API.Contracts;
using WebApplication1.Extensions;
using WebApplication1.Services;


namespace WebApplication1.Pages.AdminSearch
{
    public class SearchCoursesModel : PageModel
    {
        private readonly ICoursesRepository _coursesRespository;

        [BindProperty]
        public CourseOrderByOptions OrderOption { get; set; }
        [BindProperty]
        public CourseFilterByOptions FilterOption { get; set; }
        [BindProperty]
        public string Filter { get; set; }

        public List<Course> CoursesToShow { get; set; }


        public SearchCoursesModel(ICoursesRepository coursesRespository)
        {
            _coursesRespository = coursesRespository;
            CoursesToShow = _coursesRespository.GetAllAsync().Result;
        }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();
            CoursesToShow = _coursesRespository.SortFilterCourses(OrderOption, FilterOption, Filter);
            return Page();
        }
    }
}
