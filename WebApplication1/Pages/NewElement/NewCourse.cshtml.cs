using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using WebApplication1.Services;
using WebApplication1.Services.CourseOps;
using WebApplication1.Services.ProfessorOps;

namespace WebApplication1.Pages
{
    public class NewCourseModel : PageModel
    {
        private readonly ICreateCourseOp _createCourseOp;
        private readonly IReadProfessorOp _readProfessorOp;
        private readonly IReadCourseOp _readCourseOp;

        [BindProperty]
        public Course CreatedCourse { get; set; }
        //TODO error message for dropdown list
        [BindProperty]
        [Required(ErrorMessage ="Please select a professor or create a new one")]
        public string ProfessorIdCode { get; set; }
        public IEnumerable<SelectListItem> CreatedProfessors { get; set; }

        public NewCourseModel(ICreateCourseOp createCourseOp, IReadProfessorOp readProfessorOp, IReadCourseOp readCourseOp)
        {
            _createCourseOp = createCourseOp;
            _readProfessorOp = readProfessorOp;
            _readCourseOp = readCourseOp;
            CreatedProfessors = _readProfessorOp.GetAllProfessors().Select(p => new SelectListItem() { Text = p.ToString() + ", " + p.Subject, Value = p.IdCode });
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();
            Course course = CreateCourse();
            var courseCode = await _createCourseOp.AddCourseAsync(course);
            return RedirectToPage("/ShowResults/ShowCourse", new { courseCode = courseCode });
        }                            

        private Course CreateCourse()
        {
            return new Course()
            {
                Name = CreatedCourse.Name,
                CourseCode = CreatedCourse.CourseCode,
                Professor = _readProfessorOp.GetProfessorByIdCode(ProfessorIdCode),
                ECTS = CreatedCourse.ECTS,
                IsFinishedWithExam = CreatedCourse.IsFinishedWithExam
            };
        }
    }
}
