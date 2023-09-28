using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Contracts;
using WebApplication1.Services;

namespace WebApplication1.Pages.ShowResults
{
    [Authorize]
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

        public async Task<IActionResult> OnGet(Guid id)
        {
            CourseToShow = await _courseRespository.GetAsync(id);
            if (CourseToShow == null)
                throw new Exception("Couldn't find the course");

            var isAdmin = await _authService.AuthorizeAsync(User, "HasAdminRights");
            HasAdminRights = isAdmin.Succeeded;

            return Page();
        }
    }
}
