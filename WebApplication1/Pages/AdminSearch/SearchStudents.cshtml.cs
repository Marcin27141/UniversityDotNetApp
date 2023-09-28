using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using WebApplication1.Contracts;
using WebApplication1.Extensions;
using WebApplication1.Services.People;

namespace WebApplication1.Pages.AdminSearch
{
    [Authorize("HasAdminRights")]
    public class SearchStudentsModel : PageModel
    {
        private readonly IStudentsRepository _studentsRepository;

        [BindProperty]
        public StudentOrderByOptions OrderOption { get; set; }
        [BindProperty]
        public StudentFilterByOptions FilterOption { get; set; }
        [BindProperty]
        public string Filter { get; set; }

        public List<Student> StudentsToShow { get; set; }


		public SearchStudentsModel(IStudentsRepository studentsRepository)
        {
            _studentsRepository = studentsRepository;
            StudentsToShow = _studentsRepository.GetAllAsync().Result;
        }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
			if (!ModelState.IsValid)
				return Page();
			StudentsToShow = _studentsRepository.SortFilterStudents(OrderOption, FilterOption, Filter);
			return Page();
		}
    }
}
