using COJ.Web.Domain.Entities;
using COJ.Web.Domain.Models;

using MediatR;

namespace COJ.Web.Infraestructure.MediatR.Commands;

public class SignInAccountCommand : SignInModel, IRequest<Account>
{
    public string IpAddress { get; set; }
}

