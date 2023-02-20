using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using WebApplication1.Services.CourseOps;
using WebApplication1.Services.People;
using WebApplication1.Services.StudentOps;
using WebApplication1.Services.UserOps;

namespace WebApplication1.Pages
{
    [Authorize("HasAdminRights")]
    public class NewStudentModel : PageModel
    {
        private readonly ICreateStudentOp _createStudentOp;
        private readonly IReadCourseOp _readCourseOp;
        private readonly IReadStudentOp _readStudentOp;
        private readonly IReadUserOp _readUserOp;

        public IEnumerable<SelectListItem> ApplicationUsers { get; set; }
        public IEnumerable<SelectListItem> AvailableCourses { get; set; }

        [Required]
        [BindProperty]
        public PersonalData PersonalData { get; set; }
        [BindProperty]
        public Student Student { get; set; }

        [BindProperty]
        public IEnumerable<string> SelectedCourses { get; set; }

        [Required]
        [BindProperty]
        public string ApplicationUserId { get; set; }

        public NewStudentModel(ICreateStudentOp createStudentOp, IReadCourseOp readCourseOp, IReadStudentOp readStudentOp, IReadUserOp readUserOp) {
            _createStudentOp = createStudentOp;
            _readCourseOp = readCourseOp;
            _readStudentOp = readStudentOp;
            _readUserOp = readUserOp;
            ApplicationUsers = readUserOp.GetAllUsers().Select(p => new SelectListItem() { Text = p.ToString() + ", " + p.Id, Value = p.Id });
            AvailableCourses = _readCourseOp.GetAllCourses().Select(c => new SelectListItem() { Text = c.ToString(), Value = c.CourseCode });
        }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (_readStudentOp.GetStudentByIndex(Student.Index) != null)
                ModelState.AddModelError("Student.Index", "Student with given index is already added");
            if (_readUserOp.GetUserById(ApplicationUserId) != null)
                ModelState.AddModelError("ApplicationUserId", "There already is a connected account");
            if (!ModelState.IsValid)
                return Page();
            Student student = CreateStudent();
            var index = await _createStudentOp.AddStudentAsync(student, SelectedCourses.Where(c => c != null));
            return RedirectToPage("/ShowResults/ShowStudent", new { index = index });
        }

        private Student CreateStudent()
        {
            PersonalData.ApplicationUser = _readUserOp.GetUserById(ApplicationUserId);
            return new Student()
            {
                PersonalData = PersonalData,
                Index = Student.Index,
                BeginningOfStudying = Student.BeginningOfStudying
            };
        }
    }
}
