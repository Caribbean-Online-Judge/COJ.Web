using COJ.Web.Domain.Models;

namespace COJ.Web.Domain.Abstract;

public interface IMessageBrokerService
{
    public Task EnqueueProblemSubmissionToJudging(SubmissionJudge submissionJudge);
}