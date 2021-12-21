using System.Collections;
using COJ.Web.Domain.MediatR;
using MediatR;

namespace COJ.Web.Infrastructure.MediatR.Queries;

public class GetAllLocalesQuery : IRequest<Result<IEnumerable>>
{
    public bool IsForPublic { get; set; }
}