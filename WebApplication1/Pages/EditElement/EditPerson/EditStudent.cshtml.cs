using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ApiDtoLibrary.Students;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication1.Contracts;
using WebApplication1.Services.People;

namespace WebApplication1.Pages.EditElement.EditPerson
{
    public class EditStudentModel : PageModel
    {
        private readonly IStudentsRepository _studentsRepository;
        private readonly ICoursesRepository _coursesRepository;
        private readonly IAuthorizationService _authService;

        public IEnumerable<SelectListItem> AvailableCourses { get; set; }

        [Required]
        [BindProperty]
        public PersonalData PersonalData { get; set; }
        [BindProperty]
        public Student Student { get; set; }

        private IEnumerable<string> _selectedCourses;

        [BindProperty]
        public IEnumerable<string> SelectedCourses
        {
            get { return _selectedCourses?.Where(c => c != null); }
            set { _selectedCourses = value; }
        }

        public EditStudentModel(IStudentsRepository studentsRepository, ICoursesRepository coursesRepository, IAuthorizationService authorizationService)
        {
            _studentsRepository = studentsRepository;
            _coursesRepository = coursesRepository;
            _authService = authorizationService;
            AvailableCourses = _coursesRepository.GetAllAsync().Result.Select(i => new SelectListItem() { Text = i.ToString(), Value = i.EntityCourseID.ToString() });
        }

        public async Task<IActionResult> OnGet(Guid id)
        {
            this.Student = await _studentsRepository.GetAsync(id);

            if (this.Student == null) return NotFound(id);

            this.PersonalData = Student.PersonalData;
            SelectedCourses = Student.Courses.Select(i => i.EntityCourseID.ToString());

            var authResult = await _authService.AuthorizeAsync(User, Student, "CanEditStudent");
            if (!authResult.Succeeded)
            {
                return new ForbidResult();
            }
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            this.Student.PersonalData = this.PersonalData;
            var id = await _studentsRepository.UpdateStudentWithCoursesAsync(this.Student, SelectedCourses.Select(Guid.Parse));
            return RedirectToPage("/ShowResults/ShowStudent", new { id });
        }
    }
}
