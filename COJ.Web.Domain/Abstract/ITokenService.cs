using COJ.Web.Domain.Entities;

namespace COJ.Web.Domain.Abstract;

public interface ITokenService
{
    public RefreshToken GenerateRefreshToken(string ipAddress = "");
}