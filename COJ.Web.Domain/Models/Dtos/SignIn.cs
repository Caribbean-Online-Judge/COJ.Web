using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COJ.Web.Domain.Models.Dtos
{
    public record SignInResponse (string Token, string RefreshToken, int ExpirationTime);
}
