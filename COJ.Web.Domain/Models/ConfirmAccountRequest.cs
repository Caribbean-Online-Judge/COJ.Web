namespace COJ.Web.Domain.Models;

public class ConfirmAccountRequest
{
    public string Token { get; set; }
    public string Email { get; set; }
}