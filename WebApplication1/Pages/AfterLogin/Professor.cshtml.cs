using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApplication1.Contracts;
using WebApplication1.Services.People;

namespace WebApplication1.Pages.AfterLogin
{
    [Authorize]
    public class ProfessorModel : PageModel
    {
        private readonly IProfessorsRepository _professorsRepository;
        public Professor Professor { get; set; }

        public ProfessorModel(IProfessorsRepository professorsRepository)
        {
            _professorsRepository = professorsRepository;
        }

        public async Task OnGetAsync()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            this.Professor = await _professorsRepository.GetByUserAsync(userId);
            await _professorsRepository.SubscribeForCourseAssignments();
        }
    }
}
