using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using WebApplication1.Services.People;
using WebApplication1.Services.StudentOps;

namespace WebApplication1.Pages
{
    public class NewStudentModel : PageModel
    {
        private readonly ICreateStudentOp _createStudentOp;

        public IEnumerable<SelectListItem> AvailableCourses { get; set; }

        [Required]
        [BindProperty]
        public PersonalData PersonalData { get; set; }
        [BindProperty]
        public Student Student { get; set; }

        [BindProperty]
        public IEnumerable<string> SelectedCourses { get; set; }

        public NewStudentModel(ICreateStudentOp createStudentOp) {
            _createStudentOp = createStudentOp;
        }
        public void OnGet()
        {
            AvailableCourses = _createStudentOp.GetAvailableCourses().Select(c => new SelectListItem() { Text = c.ToString(), Value = c.CourseCode });
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();
            Student student = CreateStudent();
            var index = await _createStudentOp.AddStudentAsync(student, SelectedCourses.Where(c => c != null));
            return RedirectToPage("/ShowResults/ShowStudent", new { index = index });
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
