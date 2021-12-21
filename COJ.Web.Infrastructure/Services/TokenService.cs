using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using COJ.Web.Domain.Abstract;
using COJ.Web.Domain.Entities;
using COJ.Web.Infrastructure.Environment;
using Microsoft.IdentityModel.Tokens;

namespace COJ.Web.Infrastructure.Services;

public sealed class TokenService : ITokenService
{
    private readonly AppEnvironment _environment;

    public TokenService(AppEnvironment environment)
    {
        _environment = environment;
    }

    public RefreshToken GenerateRefreshToken(string ipAddress = "")
    {
        using var randomNumberGenerator = RandomNumberGenerator.Create();
        var randomBytes = new byte[64];
        randomNumberGenerator.GetBytes(randomBytes);
        return new RefreshToken
        {
            Token = Convert.ToBase64String(randomBytes),
            Expires = DateTime.UtcNow.AddDays(1),
            Created = DateTime.UtcNow
        };
    }

    public bool ValidateJwtToken(string token)
    {
        var mySecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_environment.Jwt.Secret));

        var tokenHandler = new JwtSecurityTokenHandler();
        try
        {
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = _environment.Jwt.ValidateIssuer,
                ValidateAudience = _environment.Jwt.ValidateAudience,
                ValidIssuers = _environment.Jwt.ValidIssuers,
                ValidAudiences = _environment.Jwt.ValidAudiences,
                IssuerSigningKey = mySecurityKey
            }, out var validatedToken);
        }
        catch
        {
            return false;
        }

        return true;
    }

    /// <summary>
    /// Check if has a role
    /// </summary>
    /// <param name="token"></param>
    /// <param name="role"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public bool HasRole(string token, string role)
    {
        var roleClaimValue = GetJwtTokenClaim(token, ClaimTypes.Role);
        var roles = roleClaimValue.Split(",");
        return roles.Any(x => x == role);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="token"></param>
    /// <param name="claimType"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public string GetJwtTokenClaim(string token, string claimType)
    {
        var claimDefault = claimType.Split("/").Last();
        var tokenHandler = new JwtSecurityTokenHandler();
        var securityToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

        var stringClaimValue = securityToken?.Claims.SingleOrDefault(claim => claim.Type == claimDefault)?.Value;
        return stringClaimValue ?? throw new InvalidOperationException();
    }
}