using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using WebApplication1.Services.PeopleOps;
using WebApplication1.Services.ProfessorOps;
using WebApplication1.Services.StudentOps;

namespace WebApplication1.Pages.ShowResults
{
    public class ShowPeopleModel : PageModel
    {
        private readonly IReadPeopleOp _readPeopleOp;
        private readonly IDeleteStudentOp _deleteStudentOp;
        private readonly IDeleteProfessorOp _deleteProfessorOp;
        public List<KeyTypePersonalData> PeopleToShow { get; set; }

        public ShowPeopleModel(IReadPeopleOp readPeopleOp, IDeleteStudentOp deleteStudentOp, IDeleteProfessorOp deleteProfessorOp)
        {
            _readPeopleOp = readPeopleOp;
            _deleteStudentOp = deleteStudentOp;
            _deleteProfessorOp = deleteProfessorOp;
        }

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
            else PeopleToShow = _readPeopleOp.GetAllPersonalData();
        }

        public async Task<IActionResult> OnGetDelete(int personType, string id)
        {
            switch ((PersonType)personType)
            {
                case PersonType.Student:
                    await _deleteStudentOp.DeleteStudentByIndexAsync(id);
                    break;
                case PersonType.Professor:
                    await _deleteProfessorOp.DeleteProfessorByIdCodeAsync(id);
                    break;
            }
            return RedirectToPage();
        }
    }
}
