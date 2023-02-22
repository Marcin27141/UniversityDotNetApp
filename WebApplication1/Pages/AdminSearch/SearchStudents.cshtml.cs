using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebApplication1.Queries;
using WebApplication1.Services.People;
using WebApplication1.Services.StudentOps;

namespace WebApplication1.Pages.AdminSearch
{
    public class SearchStudentsModel : PageModel
    {
        private readonly IReadStudentOp _readStudentOp;

        [BindProperty]
        public StudentOrderByOptions OrderOption { get; set; }
        [BindProperty]
        public StudentFilterByOptions FilterOption { get; set; }
        [BindProperty]
        public string Filter { get; set; }

        public List<Student> StudentsToShow { get; set; }


		public SearchStudentsModel(IReadStudentOp readStudentOp)
        {
            _readStudentOp = readStudentOp;
            StudentsToShow = _readStudentOp.GetAllStudents();
        }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
			if (!ModelState.IsValid)
				return Page();
			StudentsToShow = _readStudentOp.SortFilterStudents(OrderOption, FilterOption, Filter);
			return Page();
		}
    }
}
