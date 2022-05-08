using COJ.Web.Domain.Entities;
using COJ.Web.Domain.Models;
using COJ.Web.Infrestructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace COJ.Web.Infrastructure.MediatR.Queries;

public sealed class GetProblemsPaginatedQuery : IRequest<PaginatedResult<object>>
{
    public GetProblemsPaginatedQuery()
    {
        SearchBy = string.Empty;
    }

    public int Page { get; set; }
    public int PageSize { get; set; }
    public string SearchBy { get; internal set; }
    public string OrderBy { get; internal set; }
}

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
        var search = request.SearchBy;

        var total = _db.Problems.Count();
        IQueryable<object> result;

        var query = !string.IsNullOrEmpty(request.SearchBy) ? _db.Problems.Include(x => x.Translations)
                .Where(problem =>
                     problem.Translations.Any(x => x.Title.Contains(request.SearchBy))
                     || problem.Translations.Any(x => x.Description.Contains(request.SearchBy))
                     || problem.Id.ToString().Contains(request.SearchBy)
                ) : _db.Problems;

        if (request.OrderBy != null && request.OrderBy.Length > 0)
        {
         
        }

        result = query
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
            Items = result.ToList(),
            Count = result.Count(),
            Page = request.Page,
            Total = total,
        });
    }
}

