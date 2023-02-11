using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using WebApplication1.DataBase.Entities;
using WebApplication1.Policies.Requirements;

namespace WebApplication1.Policies.Handlers
{
    public class ProfessorEditorIsOwnerHandler : AuthorizationHandler<CanEditProfessorRequirement, Services.People.Professor>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public ProfessorEditorIsOwnerHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }


        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, CanEditProfessorRequirement requirement, Services.People.Professor resource)
        {
            var appUser = await _userManager.GetUserAsync(context.User);
            if (appUser == null)
                return;
            if (resource.PersonalData.ApplicationUser?.Id == appUser.Id)
                context.Succeed(requirement);
        }
    }
}
