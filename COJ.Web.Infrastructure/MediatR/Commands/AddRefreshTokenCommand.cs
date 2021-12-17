using COJ.Web.Domain.Entities;
using MediatR;

namespace COJ.Web.Infrastructure.MediatR.Commands;

public class AddRefreshTokenCommand : IRequest
{
    public RefreshToken RefreshToken { get; set; }
    public Account Account { get; set; }
}