using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using WebApplication1.Contracts;
using WebApplication1.Services.People;

namespace WebApplication1.Pages.ShowResults
{
    [Authorize("HasAdminRights")]
    public class ShowPeopleModel : PageModel
    {
        private readonly IPeopleRepository _peopleRepository;
        private readonly IUserRepository _userRepository;

        public List<Person> PeopleToShow { get; set; }

        public ShowPeopleModel(IPeopleRepository peopleRepository, IUserRepository userRepository)
        {
            _peopleRepository = peopleRepository;
            this._userRepository = userRepository;
        }

        public void OnGet()
        {
            PeopleToShow = _peopleRepository.GetAllPersonalData();
        }

        public async Task<IActionResult> OnGetDelete(Guid id)
        {
            var person = await _peopleRepository.GetPerson(id);
            await _userRepository.DeleteUserAsync(person);
            return RedirectToPage();
        }
    }
}
