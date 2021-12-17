using System.Security.Cryptography;
using COJ.Web.Domain.Abstract;
using COJ.Web.Domain.Entities;

namespace COJ.Web.Infrastructure.Services;

public sealed class TokenService : ITokenService
{
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
}