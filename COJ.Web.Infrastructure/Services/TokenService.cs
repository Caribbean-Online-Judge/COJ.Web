using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using COJ.Web.Domain.Abstract;
using COJ.Web.Domain.Entities;
using COJ.Web.Domain.Values;
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
        return new RefreshToken
        {
            Token = GenerateNewToken(),
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

    public string ComputeJwtToken(Account account, out int expirationTime)
    {
        if (account.Permissions == null)
            throw new ArgumentNullException(nameof(account));

        var roles = string.Join(",", account.Permissions.Select(x => x.Permission).ToArray());

        var symmetricKey = Encoding.UTF8.GetBytes(_environment.Jwt.Secret);
        var tokenHandler = new JwtSecurityTokenHandler();

        var now = DateTime.UtcNow;
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, account.Username),
                new Claim(ClaimTypes.Email, account.Email),
                new Claim(ClaimTypes.Role, roles),
                new Claim(ClaimTypes.NameIdentifier, $"{account.Id}")
            }),

            Expires = now.AddMinutes(_environment.Jwt.ExpirationTime),

            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(symmetricKey),
                SecurityAlgorithms.HmacSha256Signature)
        };

        var stoken = tokenHandler.CreateToken(tokenDescriptor);
        var token = tokenHandler.WriteToken(stoken);

        expirationTime = _environment.Jwt.ExpirationTime;

        return token;
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

    public AccountToken GenerateAccountToken(AccountTokenType type)
    {
        return new AccountToken
        {
            Token = GenerateNewToken(),
            ExpirationTime = DateTime.UtcNow.AddDays(1),
            Type = type,
        };
    }
    
    private static string GenerateNewToken()
    {
        using var randomNumberGenerator = RandomNumberGenerator.Create();
        var randomBytes = new byte[64];
        randomNumberGenerator.GetBytes(randomBytes);
        return Convert.ToBase64String(randomBytes);
    }
}