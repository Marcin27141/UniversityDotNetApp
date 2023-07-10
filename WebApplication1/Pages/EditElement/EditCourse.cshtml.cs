using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using UniversityApi.API.Contracts;
using WebApplication1.Services;

namespace WebApplication1.Pages.EditElement
{
    [Authorize("HasAdminRights")]
    public class EditCourseModel : PageModel
    {
        private readonly ICoursesRepository _coursesRepository;
        private readonly IProfessorsRepository _professorsRepository;

        public IEnumerable<SelectListItem> CreatedProfessors { get; set; }

        [BindProperty]
        public Course Course { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Please select a professor or create a new one")]
        public string ProfessorId { get; set; }

        public EditCourseModel(ICoursesRepository coursesRepository, IProfessorsRepository professorsRepository)
        {
            _coursesRepository = coursesRepository;
            _professorsRepository = professorsRepository;
            CreatedProfessors = _professorsRepository.GetAllAsync().Result.Select(p => new SelectListItem() { Text = p.ToString() + ", " + p.Subject, Value = p.EntityPersonID.ToString() });
        }

        public async Task OnGetAsync(Guid id)
        {
            Course = await _coursesRepository.GetAsync(id);
            ProfessorId = Course?.Professor?.IdCode;
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            this.Course.Professor = await _professorsRepository.GetAsync(Guid.Parse(ProfessorId));
            var id = await _coursesRepository.UpdateAsync(this.Course);
            return RedirectToPage("/ShowResults/ShowCourse", new { id });
        }
    }
}
