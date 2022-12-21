using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using WebApplication1.Services.People;
using WebApplication1.Services.ProfessorOps;

namespace WebApplication1.Pages.NewPerson
{
    public class NewProfessorModel : PageModel
    {
        private readonly ICreateProfessorOp _createProfessorOp;
        private readonly IReadProfessorOp _readProfessorOp;

        [Required]
        [BindProperty]
        public PersonalData PersonalData { get; set; }
        [BindProperty]
        public Professor Professor { get; set; }

        public NewProfessorModel(ICreateProfessorOp createProfessorOp, IReadProfessorOp readProfessorOp) {
            _createProfessorOp = createProfessorOp;
            _readProfessorOp = readProfessorOp;
        }


        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            if (_readProfessorOp.GetProfessorByIdCode(Professor.IdCode) != null)
                ModelState.AddModelError("Professor.IdCode", "Professor with given id is already added");
            if (!ModelState.IsValid)
                return Page();
            Professor professor = CreateProfessor();
            var idCode = await _createProfessorOp.AddProfessorAsync(professor);
            return RedirectToPage("/ShowResults/ShowProfessor", new { idCode = idCode });
        }

        private Professor CreateProfessor() 
        {
            return new() {
                PersonalData = PersonalData,
                IdCode = Professor.IdCode,
                Subject = Professor.Subject,
                FirstDayAtJob = Professor.FirstDayAtJob,
                Salary = Professor.Salary
            };
        }
    }
}
