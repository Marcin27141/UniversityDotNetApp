using Google.Api;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApplication1.Contracts;
using WebApplication1.Services;

namespace WebApplication1.Pages
{
    [Authorize]
    public class NotificationsModel : PageModel
    {
        private readonly IPeopleRepository _peopleRepository;

        public IList<Notification> Notifications { get; set; }

        public NotificationsModel(IPeopleRepository peopleRepository)
        {
            this._peopleRepository = peopleRepository;
        }

        public async Task OnGetAsync()
        {
            var entityPersonId = User.FindFirst(c => c.Type == "EntityPersonId")?.Value;
            if (entityPersonId != null)
            {
                Notifications = await _peopleRepository.GetNotifications(entityPersonId);
            }
        }
    }
}
