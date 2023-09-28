using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Contracts;
using WebApplication1.Services;
using WebApplication1.Services.People;

namespace WebApplication1.Pages.ShowResults
{
    [Authorize]
    public class ShowStudentModel : PageModel
    {
        private readonly IStudentsRepository _studentsRepository;
        public Student StudentToShow { get; set; }
        public List<Course> Courses { get; set; }
        public ShowStudentModel(IStudentsRepository studentsRepository) {
            _studentsRepository = studentsRepository;
        }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            StudentToShow = await _studentsRepository.GetAsync(id);
            if (StudentToShow == null)
                throw new Exception("Couldn't find a student with given index");
            return Page();
        }

        public async Task<IActionResult> OnGetDeleteAsync(Guid studentId, Guid courseId)
        {
            await _studentsRepository.RemoveStudentCourseAsync(studentId, courseId);
            return RedirectToPage();
        }
    }
}
