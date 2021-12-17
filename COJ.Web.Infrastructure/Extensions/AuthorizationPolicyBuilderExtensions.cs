using System.Security.Claims;
using COJ.Web.Domain.Values;
using Microsoft.AspNetCore.Authorization;

namespace COJ.Web.Infrastructure.Extensions;

public static class AuthorizationPolicyBuilderExtensions
{
    public static AuthorizationPolicyBuilder RequirePermission(this AuthorizationPolicyBuilder builder, string permission)
    {
        return builder.RequireAssertion(context =>
        {
            var permissions = context.User.Claims.SingleOrDefault(x => x.Type == ClaimTypes.Role)?.Value
                .Split(",", StringSplitOptions.RemoveEmptyEntries);

            return permissions != null && permissions.Any(x => x == AccountPermissions.CreateProblem);
        });
    }
}