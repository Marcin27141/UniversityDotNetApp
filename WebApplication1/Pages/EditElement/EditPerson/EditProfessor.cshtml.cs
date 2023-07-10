using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UniversityApi.API.Contracts;
using WebApplication1.Services.People;

namespace WebApplication1.Pages.EditElement.EditPerson
{
    public class EditProfessorModel : PageModel
    {
        private readonly IProfessorsRepository _professorsRepository;
        private readonly IAuthorizationService _authService;

        [Required]
        [BindProperty]
        public PersonalData PersonalData { get; set; }
        [BindProperty]
        public Professor Professor { get; set; }

        public EditProfessorModel(IProfessorsRepository professorsRepository, IAuthorizationService authService)
        {
            _professorsRepository = professorsRepository;
            _authService = authService;
        }
        public async Task<IActionResult> OnGet(Guid id)
        {
            Professor = await _professorsRepository.GetAsync(id);
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


            this.Professor.PersonalData = this.PersonalData;
            var id = await _professorsRepository.UpdateAsync(this.Professor);
            return RedirectToPage("/ShowResults/ShowProfessor", new { id });
        }
    }
}
