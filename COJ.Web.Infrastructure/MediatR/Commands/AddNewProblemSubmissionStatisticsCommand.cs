using COJ.Web.Domain.Entities;
using COJ.Web.Domain.MediatR;
using MediatR;

namespace COJ.Web.Infrastructure.MediatR.Commands;

public class AddNewProblemSubmissionStatisticsCommand : IRequest<Result<bool>>
{
    public int ProblemId { get; set; }
}