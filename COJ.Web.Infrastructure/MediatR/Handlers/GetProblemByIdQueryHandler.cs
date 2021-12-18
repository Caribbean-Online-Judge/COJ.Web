using COJ.Web.Domain.Exceptions;
using COJ.Web.Domain.MediatR;
using COJ.Web.Domain.Models;
using COJ.Web.Infrastructure.MediatR.Queries;
using COJ.Web.Infrastructure.Utils;
using COJ.Web.Infrestructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace COJ.Web.Infrastructure.MediatR.Handlers;

public class GetProblemByIdQueryHandler : IRequestHandler<GetProblemByIdQuery, Result<ProblemFeatures>>
{
    private readonly MainDbContext _db;

    public GetProblemByIdQueryHandler(MainDbContext db)
    {
        _db = db;
    }

    public Task<Result<ProblemFeatures>> Handle(GetProblemByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var problem = _db.Problems
                .Include(x => x.Translations)
                .ThenInclude(translation => translation.Locale)
                .Include(x => x.Classification)
                .Include(x => x.ProblemStatistics)
                .SingleOrDefault(x => x.Id == request.Id);

            if (problem == null)
                throw new InvalidOperationException();

            var localesAvailable = problem.Translations.Select(x => x.Locale.Code).ToList();

            if (!problem.ExistTranslationFor(request.Locale))
                throw new NotExistTranslationException();

            var problemsDetails = new ProblemFeatures
            {
                Id = problem.Id,
                Title = problem.GetLocalizedTitle(request.Locale),
                Description = problem.GetLocalizedDescription(request.Locale),
                CreatedAt = problem.CreatedAt,

                Statistics = new ProblemDetailsStatistics()
                {
                    Points = problem.Points,
                    AcceptedCount = problem.ProblemStatistics.Accepted,
                    ShippingCount = problem.ProblemStatistics.Shipping,
                    AcceptedPercent = MathUtils.ComputePercent(
                        problem.ProblemStatistics.Accepted,
                        problem.ProblemStatistics.Shipping
                    )
                },
                Limits = new ProblemDetailsLimits
                {
                    MemoryLimit = problem.MemoryLimit,
                    OutputLimit = problem.OutputLimit,
                    TotalTimeLimit = problem.TotalTimeLimit,
                    SourceCodeLengthLimit = problem.SourceCodeLengthLimit,
                    TestCaseTimeLimit = problem.CaseTimeLimit,
                },
                LocalesAvailable = localesAvailable
            };

            return Task.FromResult(new Result<ProblemFeatures>()
            {
                Value = problemsDetails
            });
        }
        catch (Exception e)
        {
            return Task.FromResult(new Result<ProblemFeatures>()
            {
                HasError = true,
                Exception = e
            });
        }
    }
}