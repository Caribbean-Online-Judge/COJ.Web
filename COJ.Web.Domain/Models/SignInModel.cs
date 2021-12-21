using System.ComponentModel.DataAnnotations;

namespace COJ.Web.Domain.Models;

public class SignInModel
{
    [Required] public string UsernameOrEmail { get; set; }
    [Required] public string Password { get; set; }
}