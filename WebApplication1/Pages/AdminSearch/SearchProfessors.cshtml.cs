using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using WebApplication1.Contracts;
using WebApplication1.Extensions;
using WebApplication1.Services.People;

namespace WebApplication1.Pages.AdminSearch
{
    public class SearchProfessorsModel : PageModel
    {
        private readonly IProfessorsRepository _professorsRepository;

        [BindProperty]
        public ProfessorOrderByOptions OrderOption { get; set; }
        [BindProperty]
        public ProfessorFilterByOptions FilterOption { get; set; }
        [BindProperty]
        public string Filter { get; set; }

        public List<Professor> ProfessorsToShow { get; set; }

        public SearchProfessorsModel(IProfessorsRepository professorsRepository)
        {
            _professorsRepository = professorsRepository;
            ProfessorsToShow = _professorsRepository.GetAllAsync().Result;
        }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();
            ProfessorsToShow = _professorsRepository.SortFilterProfessors(OrderOption, FilterOption, Filter);
            return Page();
        }
    }
}
