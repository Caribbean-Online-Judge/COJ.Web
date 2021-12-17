using COJ.Web.Domain.Entities;
using COJ.Web.Domain.Models;

using MediatR;

namespace COJ.Web.Infrastructure.MediatR.Queries;

public sealed class GetProblemsPaginatedQuery : IRequest<PaginatedResult<object>>
{
    public int Page { get; set; }
    public int PageSize { get; set; }
}

