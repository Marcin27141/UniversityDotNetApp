using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using WebApplication1.DataBase.Entities;
using WebApplication1.Policies.Requirements;
using WebApplication1.Services.People;

namespace WebApplication1.Policies.Handlers
{
    public class StudentEditorIsOwnerHandler : AuthorizationHandler<CanEditStudentRequrement, Services.People.Student>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public StudentEditorIsOwnerHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }


        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, CanEditStudentRequrement requirement, Services.People.Student resource)
        {
            var appUser = await _userManager.GetUserAsync(context.User);
            if (appUser == null)
                return;
            if (resource.PersonalData.ApplicationUser?.Id == appUser.Id)
                context.Succeed(requirement);
        }
    }
}
