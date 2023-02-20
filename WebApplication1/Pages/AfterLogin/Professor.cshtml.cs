using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.DataBase.Entities;
using WebApplication1.Services.People;
using WebApplication1.Services.ProfessorOps;
using WebApplication1.Services.StudentOps;

namespace WebApplication1.Pages.AfterLogin
{
    public class ProfessorModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IReadProfessorOp _readProfessorOp;
        public Professor Professor { get; set; }

        public ProfessorModel(UserManager<ApplicationUser> userManager, IReadProfessorOp readProfessorOp)
        {
            _userManager = userManager;
            _readProfessorOp = readProfessorOp;
        }

        public void OnGet()
        {
            Professor = _readProfessorOp.GetProfessorByUser(_userManager.GetUserId(User));
        }
    }
}
