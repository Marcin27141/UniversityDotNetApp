using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UniversityApi.API.Contracts;
using WebApplication1.Services;

namespace WebApplication1.Pages.ShowResults
{
    public class ShowCourseModel : PageModel
    {
        private readonly ICoursesRepository _courseRespository;
        private readonly IAuthorizationService _authService;
        public bool HasAdminRights { get; set; }
        public Course CourseToShow { get; set; }

        public ShowCourseModel(ICoursesRepository courseRespository, IAuthorizationService authService) {
            _courseRespository = courseRespository;
            _authService = authService;
        } 

        public async Task<IActionResult> OnGet(string courseCode)
        {
            CourseToShow = _courseRespository.GetByCourseCode(courseCode);
            if (CourseToShow == null)
                throw new Exception("Couldn't find the course");

            var isAdmin = await _authService.AuthorizeAsync(User, "HasAdminRights");
            HasAdminRights = isAdmin.Succeeded;

            return Page();
        }
    }
}
