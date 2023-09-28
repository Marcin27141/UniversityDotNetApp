using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Contracts;
using WebApplication1.Services.People;

namespace WebApplication1.Pages.ShowResults
{
    [Authorize]
    public class ProfessorToShowModel : PageModel
    {
        private readonly IProfessorsRepository _professorRepository;
        public Professor ProfessorToShow { get; set; }

        public ProfessorToShowModel(IProfessorsRepository professorRepository) => _professorRepository = professorRepository;

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            ProfessorToShow = await _professorRepository.GetAsync(id);
            if (ProfessorToShow == null)
                throw new Exception($"Could't find the professor with id {id}");
            return Page();
        }
    }
}
