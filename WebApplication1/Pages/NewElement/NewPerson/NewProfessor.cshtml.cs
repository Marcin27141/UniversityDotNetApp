using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using WebApplication1.Services.People;
using WebApplication1.Services.ProfessorOps;
using WebApplication1.Services.UserOps;

namespace WebApplication1.Pages.NewPerson
{
    [Authorize("HasAdminRights")]
    public class NewProfessorModel : PageModel
    {
        private readonly ICreateProfessorOp _createProfessorOp;
        private readonly IReadProfessorOp _readProfessorOp;
        private readonly IReadUserOp _readUserOp;

        public IEnumerable<SelectListItem> ApplicationUsers { get; set; }

        [BindProperty]
        public PersonalData PersonalData { get; set; }
        [BindProperty]
        public Professor Professor { get; set; }

        [Required]
        [BindProperty]
        public string ApplicationUserId { get; set; }

        public NewProfessorModel(ICreateProfessorOp createProfessorOp, IReadProfessorOp readProfessorOp, IReadUserOp readUserOp) {
            _createProfessorOp = createProfessorOp;
            _readProfessorOp = readProfessorOp;
            _readUserOp = readUserOp;
            ApplicationUsers = readUserOp.GetAllUsers().Select(p => new SelectListItem() { Text = p.ToString() + ", " + p.Id, Value = p.Id });
        }


        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            if (_readProfessorOp.GetProfessorByIdCode(Professor.IdCode) != null)
                ModelState.AddModelError("Professor.IdCode", "Professor with given id is already added");
            if (_readProfessorOp.GetProfessorByUser(ApplicationUserId) != null)
                ModelState.AddModelError("ApplicationUserId", "There already is a connected account");
            if (!ModelState.IsValid)
                return Page();
            Professor professor = CreateProfessor();
            var idCode = await _createProfessorOp.AddProfessorAsync(professor);
            return RedirectToPage("/ShowResults/ShowProfessor", new { idCode = idCode });
        }

        private Professor CreateProfessor() 
        {
            PersonalData.ApplicationUser = _readUserOp.GetUserById(ApplicationUserId);
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
