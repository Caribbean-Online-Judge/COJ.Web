using MediatR;

namespace COJ.Web.Infrastructure.MediatR.Queries;

public sealed class IsUsedAccountEmailQuery : IRequest<bool>
{
    public string Email { get; set; } = string.Empty;
}
