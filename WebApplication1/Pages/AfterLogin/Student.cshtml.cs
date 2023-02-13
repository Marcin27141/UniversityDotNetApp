using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.DataBase.Entities;
using WebApplication1.Services.People;
using WebApplication1.Services.StudentOps;

namespace WebApplication1.Pages.AfterLogin
{
    public class StudentModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IReadStudentOp _readStudentOp;
        public Services.People.Student Student { get; set; }

        public StudentModel(UserManager<ApplicationUser> userManager, IReadStudentOp readStudentOp)
        {
            _userManager = userManager;
            _readStudentOp = readStudentOp;
        }

        public void OnGet()
        {
            Student = _readStudentOp.GetStudentByUser(_userManager.GetUserId(User));
        }
    }
}
