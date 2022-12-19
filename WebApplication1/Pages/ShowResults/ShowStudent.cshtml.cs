using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Services;
using WebApplication1.Services.CourseOps;
using WebApplication1.Services.People;
using WebApplication1.Services.StudentOps;

namespace WebApplication1.Pages.ShowResults
{
    public class ShowStudentModel : PageModel
    {
        private readonly IReadStudentOp _readStudentOp;
        private readonly IUpdateStudentOp _updateStudentOp;
        public Student StudentToShow { get; set; }
        public List<Course> Courses { get; set; }
        public ShowStudentModel(IReadStudentOp readStudentOp, IUpdateStudentOp updateStudentOp) {
            _readStudentOp = readStudentOp;
            _updateStudentOp = updateStudentOp;
        }

        public IActionResult OnGet(string index)
        {
            StudentToShow = _readStudentOp.GetStudentByIndex(index);
            if (StudentToShow == null)
                throw new Exception("Couldn't find a student with given index");
            return Page();
        }

        public async Task<IActionResult> OnGetDelete(string index, string courseCode)
        {
            await _updateStudentOp.RemoveStudentCourseAsync(index, courseCode);
            return RedirectToPage();
        }
    }
}
