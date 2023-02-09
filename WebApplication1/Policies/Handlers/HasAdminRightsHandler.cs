using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using WebApplication1.Policies.Requirements;

namespace WebApplication1.Policies.Handlers
{
    public class HasAdminRightsHandler : AuthorizationHandler<CanEditStudentRequrement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CanEditStudentRequrement requirement)
        {
            if (context.User.HasClaim(claim => claim.Type == "IsAdmin"))
                context.Succeed(requirement);
            return Task.CompletedTask;
        }
    }
}
