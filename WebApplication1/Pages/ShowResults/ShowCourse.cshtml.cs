using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Services;
using WebApplication1.Services.CourseOps;
using WebApplication1.Services.People;

namespace WebApplication1.Pages.ShowResults
{
    public class ShowCourseModel : PageModel
    {
        private readonly IReadCourseOp _readCourseOp;
        private readonly IAuthorizationService _authService;
        public bool HasAdminRights { get; set; }
        public Course CourseToShow { get; set; }

        public ShowCourseModel(IReadCourseOp readCourseOp, IAuthorizationService authService) {
            _readCourseOp = readCourseOp;
            _authService = authService;
        } 

        public async Task<IActionResult> OnGet(string courseCode)
        {
            CourseToShow = _readCourseOp.GetCourseByCode(courseCode);
            if (CourseToShow == null)
                throw new Exception("Couldn't find the course");

            var isAdmin = await _authService.AuthorizeAsync(User, "HasAdminRights");
            HasAdminRights = isAdmin.Succeeded;

            return Page();
        }
    }
}
