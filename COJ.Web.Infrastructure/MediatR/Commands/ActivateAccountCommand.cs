using COJ.Web.Domain.Entities;
using COJ.Web.Domain.MediatR;
using MediatR;

namespace COJ.Web.Infrastructure.MediatR.Commands;

public class ActivateAccountCommand : IRequest<Result<Unit>>
{
    public string Email { get; set; }
    public string ActivationToken { get; set; }
}