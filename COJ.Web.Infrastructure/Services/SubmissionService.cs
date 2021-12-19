using COJ.Web.Domain.Abstract;
using COJ.Web.Domain.Entities;
using COJ.Web.Domain.MediatR;
using COJ.Web.Domain.Models;
using COJ.Web.Infrastructure.Extensions;
using COJ.Web.Infrastructure.MediatR.Commands;
using MediatR;

namespace COJ.Web.Infrastructure.Services;

public class SubmissionService : ISubmissionService
{
    private readonly IMediator _mediator;
    private readonly IMessageBrokerService _messageBrokerService;
    private readonly IProblemService _problemService;

    public SubmissionService(IMediator mediator, IMessageBrokerService messageBrokerService,
        IProblemService problemService)
    {
        _mediator = mediator;
        _messageBrokerService = messageBrokerService;
        _problemService = problemService;
    }

    public async Task<Result<ProblemSubmission>> CreateSubmission(CreateSubmissionRequest request, int accountId)
    {
        /*
         Get source code. If Source File is null so Source Code should be defined, if Source File is not null, this 
         is used instead.
        */
        var sourceCode = request.SourceCodeFile == null
            ? request.SourceCode
            : request.SourceCodeFile
                .OpenReadStream()
                .ReadToEnd();

        var result = await _mediator.Send(new CreateSubmissionCommand
        {
            LanguageId = request.LanguageId,
            ProblemId = request.ProblemId,
            SourceCode = sourceCode,
            IsForTest = request.IsForTest,
            AccountId = accountId
        });

        if (result.HasError) return result;

        var submissionToJudge = new SubmissionJudge();
        var enqueueProblemTask = _messageBrokerService.EnqueueProblemSubmissionToJudging(submissionToJudge);

        await _problemService.AddNewSubmission(request.ProblemId);

        return result;
    }

    public Task<Result<bool>> DeleteSubmission(int submissionId)
    {
        throw new NotImplementedException();
    }
}