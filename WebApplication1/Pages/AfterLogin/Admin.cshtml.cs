using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApplication1.Pages.AfterLogin
{
    [Authorize("HasAdminRights")]
    public class AdminModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
