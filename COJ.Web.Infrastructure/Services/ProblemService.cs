using COJ.Web.Domain.Abstract;
using COJ.Web.Domain.Entities;
using COJ.Web.Domain.MediatR;
using COJ.Web.Domain.Models;
using COJ.Web.Domain.Values;
using COJ.Web.Infrastructure.MediatR.Commands;
using COJ.Web.Infrastructure.MediatR.Queries;
using MediatR;

namespace COJ.Web.Infrastructure.Services;

public sealed class ProblemService : IProblemService
{
    private readonly IMediator _mediator;

    public ProblemService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<PaginatedResult<object>> GetPaginatedProblems(PaginationArguments arguments)
    {
        var problemsPaginated = await _mediator.Send(new GetProblemsPaginatedQuery()
        {
            Page = arguments.Page,
            PageSize = arguments.PageSize
        });

        return problemsPaginated;
    }

    public async Task<Problem> CreateProblem(CreateProblemRequest request)
    {
        var response = await _mediator.Send(new CreateProblemCommand()
        {
            Title = request.Title,
            Description = request.Description,
            Author = request.Author,
            Multidata = request.Multidata,
            Points = request.Points,
            MemoryLimit = request.MemoryLimit,
            CaseTimeLimit = request.CaseTimeLimit,
            OutputLimit = request.OutputLimit,
            SourceCodeLengthLimit = request.SizeLimit,
            SpecialJudge = request.SpecialJudge,
            TimeLimit = request.TimeLimit,
            ClassificationId = request.ClassificationId,
            InputFiles = request.InputFiles,
            OutputFiles = request.OutputFiles
        });

        return response;
    }

    public async Task<bool> DeleteProblemById(int id)
    {
        var response = await _mediator.Send(new DeleteProblemByIdCommand()
        {
            Id = id,
            DeleteRelatedData = true
        });

        return response;
    }

    public async Task<Result<ProblemFeatures>> GetProblemDetailsById(int id, string locale = Locales.DefaultLocale)
    {
        var details = await _mediator.Send(new GetProblemByIdQuery
        {
            Id = id,
            Locale = locale
        });

        return details;
    }

    public async Task<Result<bool>> AddNewSubmission(int problemId)
    {
        var result = await _mediator.Send(new AddNewProblemSubmissionStatisticsCommand()
        {
            ProblemId = problemId
        });

        return result;
    }

    public async Task<Result<ProblemStatistic>> GetProblemStatistics(int problemId)
    {
        return await _mediator.Send(new GetProblemStatisticsQuery
        {
            ProblemId = problemId
        });
    }
}