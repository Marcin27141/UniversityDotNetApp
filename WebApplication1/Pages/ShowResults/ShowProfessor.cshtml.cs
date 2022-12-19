using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Services.People;
using WebApplication1.Services.ProfessorOps;

namespace WebApplication1.Pages.ShowResults
{
    public class ProfessorToShowModel : PageModel
    {
        private readonly IReadProfessorOp _readProfessorOp;
        public Professor ProfessorToShow { get; set; }

        public ProfessorToShowModel(IReadProfessorOp readProfessorOp) => _readProfessorOp = readProfessorOp;

        public IActionResult OnGet(string idCode)
        {
            ProfessorToShow = _readProfessorOp.GetProfessorByIdCode(idCode);
            if (ProfessorToShow == null)
                throw new Exception("Could't find the professor with given id");
            return Page();
        }
    }
}
