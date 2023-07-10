using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using WebApplication1.Contracts;

namespace WebApplication1.Pages.ShowResults
{
    [Authorize("HasAdminRights")]
    public class ShowPeopleModel : PageModel
    {
        private readonly IPeopleRepository _peopleRepository;
        public List<KeyTypePersonalData> PeopleToShow { get; set; }

        public ShowPeopleModel(IPeopleRepository peopleRepository)
        {
            _peopleRepository = peopleRepository;
        }

        //authorization?
        //results=0 => all people; results=1 => selected people from TempData["PeopleList"]
        public void OnGet(int results)
        {
            if (results == 1)
            {
                var entry = TempData["PeopleList"];
                if (entry == null)
                    PeopleToShow = new List<KeyTypePersonalData>();
                else PeopleToShow = JsonConvert.DeserializeObject<List<KeyTypePersonalData>>(entry.ToString());
            }
            else PeopleToShow = _peopleRepository.GetAllPersonalData();
        }

        public async Task<IActionResult> OnGetDelete(Guid id)
        {
            await _peopleRepository.DeleteAsync(id);
            return RedirectToPage();
        }
    }
}
