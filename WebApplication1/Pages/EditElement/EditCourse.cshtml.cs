using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication1.Services;
using WebApplication1.Services.CourseOps;
using WebApplication1.Services.ProfessorOps;

namespace WebApplication1.Pages.EditElement
{
    [Authorize("HasAdminRights")]
    public class EditCourseModel : PageModel
    {
        private readonly IUpdateCourseOp _updateCourseOp;
        private readonly IReadProfessorOp _readProfessorOp;

        public IEnumerable<SelectListItem> CreatedProfessors { get; set; }

        [BindProperty]
        public Course Course { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Please select a professor or create a new one")]
        public string ProfessorId { get; set; }

        public EditCourseModel(IUpdateCourseOp updateCourseOp, IReadProfessorOp readProfessorOp)
        {
            _updateCourseOp = updateCourseOp;
            _readProfessorOp = readProfessorOp;
            CreatedProfessors = _readProfessorOp.GetAllProfessors().Select(p => new SelectListItem() { Text = p.ToString() + ", " + p.Subject, Value = p.IdCode });
        }

        public void OnGet(string courseCode)
        {
            Course = _updateCourseOp.GetCourseToUpdateByCode(courseCode);
            ProfessorId = Course?.Professor?.IdCode;
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
                return Page();
            var editedCourse = CreateCourse();
            string courseCode = await _updateCourseOp.UpdateCourseAsync(editedCourse);
            return RedirectToPage("/ShowResults/ShowCourse", new { courseCode });
        }

        private Course CreateCourse()
        {
            return new Course()
            {
                Name = Course.Name,
                CourseCode = Course.CourseCode,
                Professor = _readProfessorOp.GetProfessorByIdCode(ProfessorId),
                ECTS = Course.ECTS,
                IsFinishedWithExam = Course.IsFinishedWithExam
            };
        }
    }
}
