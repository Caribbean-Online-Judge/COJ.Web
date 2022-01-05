using System.Security.Claims;
using COJ.Web.Domain.Entities;
using COJ.Web.Domain.Values;

namespace COJ.Web.Domain.Abstract;

public interface ITokenService
{
    /// <summary>
    /// Compute a new token
    /// </summary>
    /// <param name="account"></param>
    /// <param name="expirationTime"></param>
    /// <returns></returns>
    string ComputeJwtToken(Account account, out int expirationTime);
    RefreshToken GenerateRefreshToken(string ipAddress = "");
    bool ValidateJwtToken(string token);
    bool HasRole(string token, string role);
    string GetJwtTokenClaim(string token, string claimType);

    AccountToken GenerateAccountToken(AccountTokenType type);
}