using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace COJ.Web.Infrastructure.Extensions;

public static class HttpContextExtensions
{
    public static int GetUserId(this HttpContext context)
    {
        var idClaimValue= context.User.Claims.SingleOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
        return int.Parse(idClaimValue!);
    }
    
    public static bool IsAnonymousRequest(this HttpContext context, out string token)
    {
        var header = context.Request.Headers.Authorization;
        token = header;
        return string.IsNullOrEmpty(header);
    }
}