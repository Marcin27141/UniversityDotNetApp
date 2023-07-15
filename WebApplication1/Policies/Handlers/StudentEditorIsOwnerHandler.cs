using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApplication1.Contracts;
using WebApplication1.Policies.Requirements;
using WebApplication1.Services.People;

namespace WebApplication1.Policies.Handlers
{
    public class StudentEditorIsOwnerHandler : AuthorizationHandler<CanEditStudentRequrement, Student>
    {
        private readonly IUserRepository _userRepository;
        public StudentEditorIsOwnerHandler(IUserRepository userRespository)
        {
            _userRepository = userRespository;
        }


        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, CanEditStudentRequrement requirement, Student resource)
        {
            var appUser = await _userRepository.GetUserAsync(context.User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (appUser == null)
                return;
            if (resource.ApplicationUserId == appUser.Id)
                context.Succeed(requirement);
        }
    }
}
