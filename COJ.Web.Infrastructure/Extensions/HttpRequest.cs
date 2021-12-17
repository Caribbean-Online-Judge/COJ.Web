using Microsoft.AspNetCore.Http;

namespace COJ.Web.Infrastructure.Extensions;

public static class HttpRequestExtensions
{
    public static string GetClientIpAddress(this HttpRequest request)
    {
        if (request.Headers.ContainsKey("X-Forwarded-For"))
            return request.Headers["X-Forwarded-For"];
        return  request.HttpContext.Connection.RemoteIpAddress?.ToString() ?? string.Empty;
    }

}

