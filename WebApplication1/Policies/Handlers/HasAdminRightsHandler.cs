using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Security.Permissions;
using System.Threading.Tasks;
using WebApplication1.Policies.Requirements;

namespace WebApplication1.Policies.Handlers
{
    public class HasAdminRightsHandler : IAuthorizationHandler
    {
        public Task HandleAsync(AuthorizationHandlerContext context)
        {
            var pendingRequirements = context.PendingRequirements.ToList();

            foreach (var requirement in pendingRequirements)
            {
                if (context.User.HasClaim(claim => claim.Type == "IsAdmin"))
                    context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
