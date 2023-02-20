using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Services;
using WebApplication1.Services.CourseOps;
using WebApplication1.Services.People;

namespace WebApplication1.Pages.ShowResults
{
    public class ShowCourseModel : PageModel
    {
        private readonly IReadCourseOp _readCourseOp;
        public Course CourseToShow { get; set; }

        public ShowCourseModel(IReadCourseOp readCourseOp) => _readCourseOp = readCourseOp;

        public IActionResult OnGet(string courseCode)
        {
            CourseToShow = _readCourseOp.GetCourseByCode(courseCode);
            if (CourseToShow == null)
                throw new Exception("Couldn't find the course");
            return Page();
        }
    }
}
