using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UniversityApi.API.Contracts;
using WebApplication1.Services;
using WebApplication1.Services.People;

namespace WebApplication1.Pages.ShowResults
{
    public class ShowStudentModel : PageModel
    {
        private readonly IStudentsRepository _studentsRepository;
        public Student StudentToShow { get; set; }
        public List<Course> Courses { get; set; }
        public ShowStudentModel(IStudentsRepository studentsRepository) {
            _studentsRepository = studentsRepository;
        }

        public IActionResult OnGet(string key)
        {
            StudentToShow = _studentsRepository.GetStudentByIndex(index);
            if (StudentToShow == null)
                throw new Exception("Couldn't find a student with given index");
            return Page();
        }

        public async Task<IActionResult> OnGetDelete(string index, string courseCode)
        {
            await _updateStudentOp.RemoveStudentCourseAsync(index, courseCode);
            return RedirectToPage();
        }
    }
}
