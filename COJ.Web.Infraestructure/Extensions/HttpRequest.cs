using Microsoft.AspNetCore.Http;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COJ.Web.Infraestructure.Extensions
{
    public static class HttpRequestExtensions
    {
        public static string GetClientIpAddress(this HttpRequest request)
        {
            string ip = request.HttpContext.Connection.RemoteIpAddress.ToString();
            return ip;
        }
    }
}
