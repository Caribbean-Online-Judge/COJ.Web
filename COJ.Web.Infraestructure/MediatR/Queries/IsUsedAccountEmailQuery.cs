using MediatR;

namespace COJ.Web.Infraestructure.MediatR.Queries;

public sealed class IsUsedAccountEmailQuery : IRequest<bool>
{
    public string Email { get; set; }
}
