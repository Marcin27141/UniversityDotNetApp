using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using WebApplication1.Services.CourseOps;
using WebApplication1.Services.People;
using WebApplication1.Services.StudentOps;

namespace WebApplication1.Pages
{
    public class NewStudentModel : PageModel
    {
        private readonly ICreateStudentOp _createStudentOp;
        private readonly IReadCourseOp _readCourseOp;
        private readonly IReadStudentOp _readStudentOp;

        public IEnumerable<SelectListItem> AvailableCourses { get; set; }

        [Required]
        [BindProperty]
        public PersonalData PersonalData { get; set; }
        [BindProperty]
        public Student Student { get; set; }

        [BindProperty]
        public IEnumerable<string> SelectedCourses { get; set; }

        public NewStudentModel(ICreateStudentOp createStudentOp, IReadCourseOp readCourseOp, IReadStudentOp readStudentOp) {
            _createStudentOp = createStudentOp;
            _readCourseOp = readCourseOp;
            _readStudentOp = readStudentOp;
        }
        public void OnGet()
        {
            AvailableCourses = _readCourseOp.GetAllCourses().Select(c => new SelectListItem() { Text = c.ToString(), Value = c.CourseCode });
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (_readStudentOp.GetStudentByIndex(Student.Index) != null)
                ModelState.AddModelError("Student.Index", "Student with given index is already added");
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
