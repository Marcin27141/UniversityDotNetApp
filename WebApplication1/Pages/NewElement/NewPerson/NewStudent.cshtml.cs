using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using UniversityApi.API.Contracts;
using WebApplication1.Contracts;
using WebApplication1.Services.People;

namespace WebApplication1.Pages
{
    [Authorize("HasAdminRights")]
    public class NewStudentModel : PageModel
    {
        private readonly IStudentsRepository _studentsRepository;
        private readonly ICoursesRepository _coursesRepository;
        private readonly IUserRepository _userRepository;

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

        public NewStudentModel(IStudentsRepository studentsRepository, ICoursesRepository coursesRepository, IUserRepository userRepository) {
            _studentsRepository = studentsRepository;
            _coursesRepository = coursesRepository;
            _userRepository = userRepository;

            ApplicationUsers = _userRepository.GetAllUsers().Select(p => new SelectListItem() { Text = p.ToString() + ", " + p.Id, Value = p.Id });
            AvailableCourses = _readCourseOp.GetAllCourses().Select(c => new SelectListItem() { Text = c.ToString(), Value = c.CourseCode });
        }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (_readStudentOp.GetStudentByIndex(Student.Index) != null)
                ModelState.AddModelError("Student.Index", "Student with given index is already added");
            if (_readStudentOp.GetStudentByUser(ApplicationUserId) != null)
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
