using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication1.Services.People;
using WebApplication1.Services.StudentOps;

namespace WebApplication1.Pages.EditElement.EditPerson
{
    public class EditStudentModel : PageModel
    {
        private readonly IUpdateStudentOp _updateStudentOp;

        public IEnumerable<SelectListItem> AvailableCourses { get; set; }

        [Required]
        [BindProperty]
        public PersonalData PersonalData { get; set; }
        [BindProperty]
        public Student Student { get; set; }

        [BindProperty]
        public IEnumerable<string> SelectedCourses { get; set; }

        public EditStudentModel(IUpdateStudentOp updateStudentOp) => _updateStudentOp = updateStudentOp;

        public void OnGet(string index)
        {
            Student = _updateStudentOp.GetStudentToUpdateByIndex(index);
            PersonalData = Student?.PersonalData;
            SelectedCourses = Student.Courses.Select(i => i.CourseCode.ToString());
            AvailableCourses = _updateStudentOp.GetAvailableCourses().Select(i => new SelectListItem() { Text = i.ToString(), Value = i.CourseCode.ToString() });
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
                return Page();
            var editedStudent = CreateStudent();
            var studentIndex = await _updateStudentOp.UpdateStudentAsync(editedStudent, SelectedCourses.Where(c => c != null));
            return RedirectToPage("/ShowResults/ShowStudent", new { index = studentIndex });
        }

        private Student CreateStudent()
        {
            return new Student()
            {
                PersonalData = PersonalData,
                Index = Student.Index,
                BeginningOfStudying = Student.BeginningOfStudying,
                Average = Student.Average
            };
        }
    }
}
