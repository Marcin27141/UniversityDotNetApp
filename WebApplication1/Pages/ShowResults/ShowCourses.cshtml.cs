using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UniversityApi.API.Contracts;
using WebApplication1.Services;
using WebApplication1.Services.CourseOps;

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
        public void OnGet()
        {
            CreatedCourses = _coursesRepository.GetAllCourses();
        }

        public async Task<IActionResult> OnGetDelete(string courseCode)
        {
            await _coursesRepository.Delete(courseCode);
            return RedirectToPage();
        }
    }
}
