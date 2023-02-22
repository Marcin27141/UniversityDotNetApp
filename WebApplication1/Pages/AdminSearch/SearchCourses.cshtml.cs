using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using WebApplication1.Queries;
using WebApplication1.Services;
using WebApplication1.Services.CourseOps;


namespace WebApplication1.Pages.AdminSearch
{
    public class SearchCoursesModel : PageModel
    {
        private readonly IReadCourseOp _readCourseOp;

        [BindProperty]
        public CourseOrderByOptions OrderOption { get; set; }
        [BindProperty]
        public CourseFilterByOptions FilterOption { get; set; }
        [BindProperty]
        public string Filter { get; set; }

        public List<Course> CoursesToShow { get; set; }


        public SearchCoursesModel(IReadCourseOp readCourseOp)
        {
            _readCourseOp = readCourseOp;
            CoursesToShow = _readCourseOp.GetAllCourses();
        }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();
            CoursesToShow = _readCourseOp.SortFilterCourses(OrderOption, FilterOption, Filter);
            return Page();
        }
    }
}
