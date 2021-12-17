using COJ.Web.Domain.Models;
using COJ.Web.Infrastructure.MediatR.Queries;
using COJ.Web.Infrestructure.Data;
using MediatR;

namespace COJ.Web.Infrastructure.MediatR.Handlers;

public sealed class
    GetProblemsPaginatedQueryHandler : IRequestHandler<GetProblemsPaginatedQuery, PaginatedResult<object>>
{
    private readonly MainDbContext _db;

    public GetProblemsPaginatedQueryHandler(MainDbContext db)
    {
        this._db = db;
    }

    public Task<PaginatedResult<object>> Handle(GetProblemsPaginatedQuery request, CancellationToken cancellationToken)
    {
        var total = _db.Problems.Count();
        var result = _db.Problems
            .OrderBy(x => x.Id)
            .Skip(request.Page * request.PageSize)
            .Take(request.PageSize)
            .Select(x => new
            {
                //x.Title,
                x.Id,
                x.Points,
            });

        return Task.FromResult(new PaginatedResult<object>()
        {
            Items = result.ToList<object>(),
            Count = result.Count(),
            Page = request.Page,
            Total = total,
        });
    }
}