using COJ.Web.Domain.Entities;
using COJ.Web.Domain.Exceptions;
using COJ.Web.Domain.MediatR;
using COJ.Web.Domain.Models;
using COJ.Web.Infrastructure.MediatR.Queries;
using COJ.Web.Infrestructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace COJ.Web.Infrastructure.MediatR.Handlers;

public class GetProblemStatisticsQueryHandler : IRequestHandler<GetProblemStatisticsQuery, Result<ProblemStatistic>>
{
    private readonly MainDbContext _db;

    public GetProblemStatisticsQueryHandler(MainDbContext db)
    {
        _db = db;
    }


    public Task<Result<ProblemStatistic>> Handle(GetProblemStatisticsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var problem = _db.Problems
                .Include(x => x.ProblemStatistics)
                .SingleOrDefault(x => x.Id == request.ProblemId);

            if (problem == null)
                throw new EntityNotFoundedException();

            return Task.FromResult(new Result<ProblemStatistic>(problem.ProblemStatistics));
        }
        catch (Exception e)
        {
            return Task.FromResult(new Result<ProblemStatistic>(e));
        }
    }
}