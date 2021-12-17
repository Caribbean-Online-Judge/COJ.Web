namespace COJ.Web.Domain.Models;

public class RefreshTokenResult
{
    public string Token { get; set; }
    public int Duration { get; set; }
    public string RefreshToken { get; set; }
}