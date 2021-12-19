using COJ.Web.Domain.Entities;
using COJ.Web.Domain.MediatR;
using MediatR;

namespace COJ.Web.Infrastructure.MediatR.Queries;

public class GetProblemStatisticsQuery : IRequest<Result<ProblemStatistic>>
{
    public int ProblemId { get; set; }
}