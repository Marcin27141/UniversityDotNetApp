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
        public List<Person> PeopleToShow { get; set; }

        public ShowPeopleModel(IPeopleRepository peopleRepository)
        {
            _peopleRepository = peopleRepository;
        }

        //authorization?
        //results=0 => all people; results=1 => selected people from TempData["PeopleList"]
        public void OnGet(int results=0)
        {
            if (results == 1)
            {
                var entry = TempData["PeopleList"];
                if (entry == null)
                    PeopleToShow = new List<Person>();
                else PeopleToShow = JsonConvert.DeserializeObject<List<Person>>(entry.ToString());
            }
            else PeopleToShow = _peopleRepository.GetAllPersonalData();
        }

        public async Task<IActionResult> OnGetDelete(Guid id)
        {
            var person = await _peopleRepository.GetPerson(id);
            await _peopleRepository.DeleteAsync(person);
            return RedirectToPage();
        }
    }
}
