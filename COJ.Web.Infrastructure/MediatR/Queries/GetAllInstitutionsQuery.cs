using System.Collections;
using COJ.Web.Domain.MediatR;
using MediatR;

namespace COJ.Web.Infrastructure.MediatR.Queries;

public class GetAllInstitutionsQuery : IRequest<Result<IEnumerable>>
{
    public bool IsForPublic { get; set; }
    public int? CountryId { get; set; }
}