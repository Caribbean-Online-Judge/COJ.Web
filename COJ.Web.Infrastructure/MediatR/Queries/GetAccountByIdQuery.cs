using COJ.Web.Domain.Entities;
using COJ.Web.Infrestructure.Data;
using MediatR;

namespace COJ.Web.Infrastructure.MediatR.Queries;

public class GetAccountByIdQuery : IRequest<Account>
{
    public int Id { get; set; }
}



