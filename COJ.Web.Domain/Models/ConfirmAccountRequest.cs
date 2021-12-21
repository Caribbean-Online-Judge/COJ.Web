using System.ComponentModel.DataAnnotations;

namespace COJ.Web.Domain.Models;

public class ConfirmAccountRequest
{
    [Required]
    public string Token { get; set; }
    [Required]
    public string Email { get; set; }
}