using COJ.Web.Domain.Entities;
using COJ.Web.Domain.MediatR;
using COJ.Web.Domain.Models;
using MediatR;

namespace COJ.Web.Domain.Abstract;

public interface ISubmissionService
{
    /// <summary>
    /// Create a new submission.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="accountId"></param>
    /// <returns></returns>
    public Task<Result<ProblemSubmission>> CreateSubmission(CreateSubmissionRequest request, int accountId);

    /// <summary>
    /// Delete a submission.
    /// </summary>
    /// <param name="submissionId">Submission id.</param>
    /// <returns></returns>
    public Task<Result<bool>> DeleteSubmission(int submissionId);
}