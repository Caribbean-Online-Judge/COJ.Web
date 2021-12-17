using COJ.Web.Domain.Entities;
using COJ.Web.Domain.Models;
using COJ.Web.Infrestructure.Data;
using MediatR;

namespace COJ.Web.Infrastructure.MediatR.Commands;

public class CreateAccountCommand : SignUpModel, IRequest<Account>
{

}