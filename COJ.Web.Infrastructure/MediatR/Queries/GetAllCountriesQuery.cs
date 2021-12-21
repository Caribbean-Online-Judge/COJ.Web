using System.Collections;
using COJ.Web.Domain.Entities;
using COJ.Web.Domain.MediatR;
using MediatR;

namespace COJ.Web.Infrastructure.MediatR.Queries;

public class GetAllCountriesQuery : IRequest<Result<IEnumerable>>
{
    public bool IsForPublic { get; set; }
}