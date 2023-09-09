using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ApiDtoLibrary.Person;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
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

        private IEnumerable<string> _selectedCourses;

        [BindProperty]
        public IEnumerable<string> SelectedCourses
        {
            get { return _selectedCourses?.Where(c => c != null); }
            set { _selectedCourses = value; }
        }

        [Required]
        [BindProperty]
        public string ApplicationUserId { get; set; }

        public NewStudentModel(IStudentsRepository studentsRepository, ICoursesRepository coursesRepository, IUserRepository userRepository) {
            _studentsRepository = studentsRepository;
            _coursesRepository = coursesRepository;
            _userRepository = userRepository;

            ApplicationUsers = _userRepository.GetUnsetNonadminUsersAsync().Result.Where(u => u.Status == PersonStatus.Student).Select(p => new SelectListItem() { Text = p.Email, Value = p.Id });
            var courses = _coursesRepository.GetAllAsync().Result;
            AvailableCourses = courses.Select(c => new SelectListItem() { Text = c.ToString(), Value = c.EntityCourseID.ToString() });
        }
        public void OnGet()
        {
            
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (await _studentsRepository.IndexIsOccupied(Student.Index))
                ModelState.AddModelError("Student.Index", "Student with given index is already added");
            if (await _studentsRepository.GetByUserAsync(ApplicationUserId) != null)
                ModelState.AddModelError("ApplicationUserId", "There already is a connected account");
            if (!ModelState.IsValid)
                return Page();

            AssignStudentProperties();
            var addedEntity = await _studentsRepository.AddStudentWithCoursesAsync(this.Student, SelectedCourses.Select(Guid.Parse));
            return RedirectToPage("/ShowResults/ShowStudent", new { id = addedEntity.EntityPersonID.ToString() });
        }

        private void AssignStudentProperties()
        {
            this.Student.PersonStatus = PersonStatus.Student;
            this.Student.ApplicationUserId = ApplicationUserId;
            this.Student.PersonalData = PersonalData;
        }
    }
}
