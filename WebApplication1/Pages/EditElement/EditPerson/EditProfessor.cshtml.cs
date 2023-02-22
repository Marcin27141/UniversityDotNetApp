using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Services.People;
using WebApplication1.Services.ProfessorOps;

namespace WebApplication1.Pages.EditElement.EditPerson
{
    public class EditProfessorModel : PageModel
    {
        private readonly IUpdateProfessorOp _updateProfessorOp;
        private readonly IAuthorizationService _authService;

        [Required]
        [BindProperty]
        public PersonalData PersonalData { get; set; }
        [BindProperty]
        public Professor Professor { get; set; }

        public EditProfessorModel(IUpdateProfessorOp updateProfessorOp, IAuthorizationService authService)
        {
            _updateProfessorOp = updateProfessorOp;
            _authService = authService;
        }
        public async Task<IActionResult> OnGet(string idCode)
        {
            Professor = _updateProfessorOp.GetProfessorToUpdateByIdCode(idCode);
            PersonalData = Professor?.PersonalData;

            var authResult = await _authService.AuthorizeAsync(User, Professor, "CanEditProfessor");
            if (!authResult.Succeeded)
            {
                return new ForbidResult();
            }
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
                return Page();
            var editedProfessor = CreateProfessor();
            var idCode = await _updateProfessorOp.UpdateProfessorAsync(editedProfessor);
            return RedirectToPage("/ShowResults/ShowProfessor", new { idCode });
        }

        private Professor CreateProfessor()
        {
            return new()
            {
                PersonalData = PersonalData,
                IdCode = Professor.IdCode,
                Subject = Professor.Subject,
                FirstDayAtJob = Professor.FirstDayAtJob,
                Salary = Professor.Salary
            };
        }
    }
}