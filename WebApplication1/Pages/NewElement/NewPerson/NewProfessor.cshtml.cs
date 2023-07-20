using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ApiDtoLibrary.Person;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using UniversityApi.API.Contracts;
using WebApplication1.Contracts;
using WebApplication1.Services.People;

namespace WebApplication1.Pages.NewPerson
{
    [Authorize("HasAdminRights")]
    public class NewProfessorModel : PageModel
    {
        private readonly IProfessorsRepository _professorsRepository;
        private readonly IUserRepository _userRepository;

        public IEnumerable<SelectListItem> ApplicationUsers { get; set; }

        [BindProperty]
        public PersonalData PersonalData { get; set; }
        [BindProperty]
        public Professor Professor { get; set; }

        [Required]
        [BindProperty]
        public string ApplicationUserId { get; set; }

        public NewProfessorModel(IProfessorsRepository professorsRepository, IUserRepository userRepository) {
            _professorsRepository = professorsRepository;
            _userRepository = userRepository;
            ApplicationUsers = _userRepository.GetUnsetNonadminUsersAsync().Result.Where(u => u.Status == PersonStatus.Professor).Select(p => new SelectListItem() { Text = p.Email, Value = p.Id });
        }


        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            if (await _professorsRepository.IdCodeIsOccupied(this.Professor.IdCode))
                ModelState.AddModelError("Professor.IdCode", "Professor with given id is already added");
            if (await _professorsRepository.GetByUserAsync(ApplicationUserId) != null)
                ModelState.AddModelError("ApplicationUserId", "There already is a connected account");
            if (!ModelState.IsValid)
                return Page();

            AssingProfessorProperties();
            var addedEntity = await _professorsRepository.AddAsync(this.Professor);
            return RedirectToPage("/ShowResults/ShowProfessor", new { id = addedEntity.EntityPersonId });
        }

        private void AssingProfessorProperties()
        {
            this.Professor.PersonStatus = PersonStatus.Professor;
            this.Professor.ApplicationUserId = ApplicationUserId;
            this.Professor.PersonalData = this.PersonalData;
        }
    }
}
