using Microsoft.AspNetCore.Mvc;

using System.Security.Claims;

namespace COJ.Web.Infrastructure.Extensions;

    public static class ControllerBaseExtensions
    {
        public static BadRequestObjectResult BadRequestWithMessage(this ControllerBase controller, string message)
        {
            return controller.BadRequest(new
            {
                Message = message,
            });
        }
    }

