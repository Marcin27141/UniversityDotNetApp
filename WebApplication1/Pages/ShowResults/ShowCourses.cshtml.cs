using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Services;
using WebApplication1.Services.CourseOps;

namespace WebApplication1.Pages.ShowResults
{
    public class ShowCoursesModel : PageModel
    {
        private readonly IReadCourseOp _readCourseOp;
        private readonly IDeleteCourseOp _deleteCourseOp;
        public List<Course> CreatedCourses { get; set; }
        public ShowCoursesModel(IReadCourseOp readCourseOp, IDeleteCourseOp deleteCourseOp)
        {
            _readCourseOp = readCourseOp;
            _deleteCourseOp = deleteCourseOp;
        }
        public void OnGet()
        {
            CreatedCourses = _readCourseOp.GetAllCourses();
        }

        public async Task<IActionResult> OnGetDelete(string courseCode)
        {
            await _deleteCourseOp.DeleteCourseByCodeAsync(courseCode);
            return RedirectToPage();
        }
    }
}
