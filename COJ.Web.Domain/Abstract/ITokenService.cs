using System.Security.Claims;
using COJ.Web.Domain.Entities;

namespace COJ.Web.Domain.Abstract;

public interface ITokenService
{
    RefreshToken GenerateRefreshToken(string ipAddress = "");
    bool ValidateJwtToken(string token);
    bool HasRole(string token, string role);
    string GetJwtTokenClaim(string token, string claimType);
}