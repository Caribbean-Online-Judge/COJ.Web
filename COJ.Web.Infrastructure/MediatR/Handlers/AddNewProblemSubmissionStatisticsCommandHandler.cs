using COJ.Web.Domain.MediatR;
using COJ.Web.Infrastructure.MediatR.Commands;
using COJ.Web.Infrestructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace COJ.Web.Infrastructure.MediatR.Handlers;

public sealed class
    AddNewProblemSubmissionStatisticsCommandHandler : IRequestHandler<AddNewProblemSubmissionStatisticsCommand,
        Result<bool>>
{
    private readonly MainDbContext _db;

    public AddNewProblemSubmissionStatisticsCommandHandler(MainDbContext db)
    {
        _db = db;
    }


    public async Task<Result<bool>> Handle(AddNewProblemSubmissionStatisticsCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            var problem = _db.Problems.Include(x => x.ProblemStatistics)
                .SingleOrDefault(x => x.Id == request.ProblemId);

            if (problem == null)
                throw new NullReferenceException();

            problem.ProblemStatistics.Shipping++;

            await _db.SaveChangesAsync(cancellationToken);

            return new Result<bool>(true);
        }
        catch (Exception e)
        {
            return new Result<bool>(e);
        }
    }
}