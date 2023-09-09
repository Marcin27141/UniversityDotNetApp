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
using Newtonsoft.Json;
using WebApplication1.Contracts;
using WebApplication1.Services;
using WebApplication1.Services.People;

namespace WebApplication1.Pages
{
    [Authorize("HasAdminRights")]
    public class NewCourseModel : PageModel
    {
        private readonly ICoursesRepository _coursesRespository;
        private readonly IProfessorsRepository _professorsRepository;

        [BindProperty]
        public Course CreatedCourse { get; set; }
#nullable enable
        //TODO error message for dropdown list
        [BindProperty]
        public string? ProfessorId { get; set; }
#nullable disable
        public IEnumerable<SelectListItem> CreatedProfessors { get; set; }

        public NewCourseModel(ICoursesRepository coursesRespository, IProfessorsRepository professorsRepository)
        {
            _coursesRespository = coursesRespository;
            _professorsRepository = professorsRepository;
            CreatedProfessors = _professorsRepository.GetAllAsync().Result.Select(p => new SelectListItem() { Text = p.ToString() + ", " + p.Subject, Value = p.EntityPersonID.ToString() });
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (await _coursesRespository.CourseCodeIsOccupied(CreatedCourse.CourseCode))
                ModelState.AddModelError("CreatedCourse.CourseCode", "Course with given code is already added");
            if (!ModelState.IsValid)
                return Page();
            if (ProfessorId != null) CreatedCourse.Professor = await _professorsRepository.GetAsync(Guid.Parse(ProfessorId));
            var addedEntity = await _coursesRespository.AddAsync(CreatedCourse);
            return RedirectToPage("/ShowResults/ShowCourse", new { id = addedEntity.EntityCourseId });
        }                            
    }
}
