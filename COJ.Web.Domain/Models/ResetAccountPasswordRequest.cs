namespace COJ.Web.Domain.Models;

public class ResetAccountPasswordRequest
{
    public string Email { get; set; }
    public string Token { get; set; }
    public string NewPassword { get; set; }
}