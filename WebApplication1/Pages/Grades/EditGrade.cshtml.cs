using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Authorization;
using WebApplication1.Contracts;
using WebApplication1.Services;
using WebApplication1.Services.People;
using GrpcService.Models;
using GrpcService.Services;

namespace WebApplication1.Pages.Grades
{
    [Authorize("HasProfessorRights")]
    public class EditGradeModel : PageModel
    {
        private readonly IGradesRepository _gradesRepository;
        private readonly IStudentsRepository _studentsRepository;

        [BindProperty]
        public CourseGrade Grade { get; set; }
        [BindProperty]
        public Services.People.Student Student { get; set; }

        public EditGradeModel(IGradesRepository gradesRepository, IStudentsRepository studentsRepository)
        {
            _gradesRepository = gradesRepository;
            this._studentsRepository = studentsRepository;
        }

        public async Task OnGetAsync(Guid gradeId)
        {
            Grade = await _gradesRepository.getGrade(gradeId);
            if (Grade != null)
                Student = await _studentsRepository.GetAsync(Grade.StudentId);
        }

        public async Task<IActionResult> OnPost()
        {
            CheckGradeValidity();
            if (!ModelState.IsValid)
                return Page();

            await _gradesRepository.updateGrade(Grade.GradeId, Grade.GradeValue);
            return RedirectToPage("/Grades/CourseGrades", new { courseId = Grade.CourseId });
        }

        private void CheckGradeValidity()
        {
            var validationResult = GrpcValidator.CheckIfGradeIsValid(Grade.GradeValue);
            if (!validationResult.WasSuccessful)
                ModelState.AddModelError("Grade.GradeValue", validationResult.Message);
        }
    }
}
