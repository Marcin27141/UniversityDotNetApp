using GrpcService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using System;
using WebApplication1.Contracts;
using WebApplication1.Services;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net;
using GrpcService.Models;

namespace WebApplication1.Pages.Grades
{
    [Authorize("IsProfessor")]
    public class AddGradeModel : PageModel
    {
        private readonly IGradesRepository _gradesRepository;
        private readonly ICoursesRepository _coursesRepository;

        [BindProperty]
        public Guid CourseId { get; set; }

        public Services.Course Course { get; set; }
        public IEnumerable<SelectListItem> CourseStudents { get; set; }

        [BindProperty]
        public CourseGrade Grade { get; set; }
        [BindProperty]
        public string StudentId { get; set; }

        public AddGradeModel(IGradesRepository gradesRepository, ICoursesRepository coursesRepository)
        {
            _gradesRepository = gradesRepository;
            this._coursesRepository = coursesRepository;
        }

        public async Task OnGetAsync(Guid courseId)
        {
            await GetCourseAndStudents(courseId);
            var professorId = User.FindFirst(c => c.Type == "EntityPersonId")?.Value;
            if (professorId == null || Course.Professor?.EntityPersonID.ToString() != professorId)
                throw new Exception("Unauthorized");           
        }

        public async Task<IActionResult> OnPost()
        {
            CheckGradeValidity();
            if (!ModelState.IsValid)
            {
                await GetCourseAndStudents(CourseId);
                return Page();
            }
                

            Grade.CourseId = CourseId;
            Grade.StudentId = Guid.Parse(StudentId);
            await _gradesRepository.addGrade(Grade);
            return RedirectToPage("/Grades/CourseGrades", new { courseId = Grade.CourseId });
        }

        private async Task GetCourseAndStudents(Guid courseId)
        {
            Course = await _coursesRepository.GetAsync(courseId);
            CourseStudents = Course.Students.Select(s => new SelectListItem() { Text = $"{s.PersonalData.FirstName} {s.PersonalData.LastName}, {s.Index}", Value = s.EntityPersonID.ToString() });
        }

        private void CheckGradeValidity()
        {
            var validationResult = GrpcValidator.CheckIfGradeIsValid(Grade.GradeValue);
            if (!validationResult.WasSuccessful)
                ModelState.AddModelError("Grade.GradeValue", validationResult.Message);
        }
    }
}
