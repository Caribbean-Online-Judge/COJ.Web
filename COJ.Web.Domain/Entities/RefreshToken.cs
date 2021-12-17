using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace COJ.Web.Domain.Entities;

public class RefreshToken
{
    [Key]
    [JsonIgnore]
    public int Id { get; set; }
        
    public string Token { get; set; }
    public DateTime Expires { get; set; }
    public DateTime Created { get; set; }
    public bool IsExpired => DateTime.UtcNow >= Expires;
}