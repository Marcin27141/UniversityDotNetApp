using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using WebApplication1.Contracts;
using WebApplication1.Services;
using System.Linq;

namespace WebApplication1.Pages.ShowResults
{
    public class ShowProfessorCoursesModel : PageModel
    {
        private readonly ICoursesRepository _coursesRepository;
        public List<Course> ProfessorCourses { get; set; }
        public ShowProfessorCoursesModel(ICoursesRepository coursesRepository)
        {
            _coursesRepository = coursesRepository;
        }
        public async Task OnGetAsync()
        {
            var professorId = User.FindFirst(c => c.Type == "EntityPersonId")?.Value;
            ProfessorCourses = await _coursesRepository.GetAllAsync();
            ProfessorCourses = ProfessorCourses.Where(c => c.ProfessorId.ToString() == professorId).ToList();
        }
    }
}
