using COJ.Web.Domain.Abstract;
using COJ.Web.Domain.Attributes;
using COJ.Web.Domain.Models;

namespace COJ.Web.Infrastructure.Services;

[InjectAsSingleton]
public class MessageBrokerService : IMessageBrokerService
{
    public async Task EnqueueProblemSubmissionToJudging(SubmissionJudge submissionJudge)
    {
        await Task.Yield();
    }
}