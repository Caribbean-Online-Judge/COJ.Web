using COJ.Web.Domain.Entities;
using COJ.Web.Domain.Models;
using MediatR;

namespace COJ.Web.Infrastructure.MediatR.Commands;

public class RefreshTokenCommand : IRequest<RefreshTokenResult?>
{
    public string RefreshToken { get; set; }
}