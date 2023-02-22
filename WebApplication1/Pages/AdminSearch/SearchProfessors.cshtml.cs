using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using WebApplication1.Queries;
using WebApplication1.Services.People;
using WebApplication1.Services.ProfessorOps;

namespace WebApplication1.Pages.AdminSearch
{
    public class SearchProfessorsModel : PageModel
    {
        private readonly IReadProfessorOp _readProfessorOp;

        [BindProperty]
        public ProfessorOrderByOptions OrderOption { get; set; }
        [BindProperty]
        public ProfessorFilterByOptions FilterOption { get; set; }
        [BindProperty]
        public string Filter { get; set; }

        public List<Professor> ProfessorsToShow { get; set; }

        public SearchProfessorsModel(IReadProfessorOp readProfessorOp)
        {
            _readProfessorOp = readProfessorOp;
            ProfessorsToShow = _readProfessorOp.GetAllProfessors();
        }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();
            ProfessorsToShow = _readProfessorOp.SortFilterProfessors(OrderOption, FilterOption, Filter);
            return Page();
        }
    }
}
