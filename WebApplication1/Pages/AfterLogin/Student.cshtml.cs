using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using System.Threading.Tasks;
using UniversityApi.API.Contracts;
using WebApplication1.Services.People;

namespace WebApplication1.Pages.AfterLogin
{
    public class StudentModel : PageModel
    {
        private readonly IStudentsRepository _studentsRepository;
        public Student Student { get; set; }

        public StudentModel(IStudentsRepository studentsRepository)
        {
            _studentsRepository = studentsRepository;
        }

        public async Task OnGetAsync()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            this.Student = await _studentsRepository.GetByUserAsync(userId);
        }
    }
}
