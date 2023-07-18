using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UniversityApi.API.Contracts;
using WebApplication1.Services;

namespace WebApplication1.Pages.ShowResults
{
    [Authorize("HasAdminRights")]
    public class ShowCoursesModel : PageModel
    {
        private readonly ICoursesRepository _coursesRepository;
        public List<Course> CreatedCourses { get; set; }
        public ShowCoursesModel(ICoursesRepository coursesRepository)
        {
            _coursesRepository = coursesRepository;
        }
        public async Task OnGetAsync()
        {
            CreatedCourses = await _coursesRepository.GetAllAsync();
        }

        public async Task<IActionResult> OnGetDelete(Guid id)
        {
            var course = await _coursesRepository.GetAsync(id);
            await _coursesRepository.DeleteAsync(course);
            return RedirectToPage();
        }
    }
}
