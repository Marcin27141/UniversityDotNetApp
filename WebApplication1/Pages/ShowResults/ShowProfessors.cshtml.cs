using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using WebApplication1.Contracts;
using Microsoft.AspNetCore.Authorization;
using WebApplication1.Services.People;

namespace WebApplication1.Pages.ShowResults
{
    [Authorize("HasAdminRights")]
    public class ShowProfessorsModel : PageModel
    {
        private readonly IProfessorsRepository _professorsRepository;
        private readonly IUserRepository _userRepository;

        public List<Professor> ProfessorsToShow { get; set; }

        public ShowProfessorsModel(IProfessorsRepository professorsRepository, IUserRepository userRepository)
        {
            _professorsRepository = professorsRepository;
            this._userRepository = userRepository;
        }

        public void OnGet()
        {
            ProfessorsToShow = _professorsRepository.GetAllAsync().Result;
        }

        public async Task<IActionResult> OnGetDelete(Guid id)
        {
            var professor = await _professorsRepository.GetAsync(id);
            await _userRepository.DeleteUserAsync(professor);
            return RedirectToPage();
        }
    }
}
