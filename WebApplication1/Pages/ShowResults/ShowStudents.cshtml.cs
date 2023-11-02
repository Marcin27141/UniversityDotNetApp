using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using WebApplication1.Contracts;
using Microsoft.AspNetCore.Authorization;
using WebApplication1.Services.People;

namespace WebApplication1.Pages.ShowResults
{
    [Authorize("HasAdminRights")]
    public class ShowStudentsModel : PageModel
    {
        private readonly IStudentsRepository _studentsRepository;
        private readonly IUserRepository _userRepository;

        public List<Student> StudentsToShow { get; set; }

        public ShowStudentsModel(IStudentsRepository studentsRepository, IUserRepository userRepository)
        {
            _studentsRepository = studentsRepository;
            this._userRepository = userRepository;
        }

        public void OnGet()
        {
            StudentsToShow = _studentsRepository.GetAllAsync().Result;
        }

        public async Task<IActionResult> OnGetDelete(Guid id)
        {
            var student = await _studentsRepository.GetAsync(id);
            await _userRepository.DeleteUserAsync(student);
            return RedirectToPage();
        }
    }
}
