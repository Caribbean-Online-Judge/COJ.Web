using Microsoft.AspNetCore.Mvc;

using System.Security.Claims;

namespace COJ.Web.Infrastructure.Extensions
{
    public static class ControllerBaseExtensions
    {
        public static string? GetAuthenticatedUsedId(this ControllerBase controller)
        {
            return controller.HttpContext.User.Claims.SingleOrDefault(x => x.Type == ClaimTypes.Name)?.Value;
        }
    }
}
