using COJ.Web.Domain.Entities;
using COJ.Web.Domain.MediatR;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace COJ.Web.Infrastructure.MediatR.Commands;

public class CreateSubmissionCommand : IRequest<Result<ProblemSubmission>>
{
    public int ProblemId { get; set; }
    public int LanguageId { get; set; }
    public string SourceCode { get; set; }
    public bool IsForTest { get; set; }
    public int AccountId { get; set; }
}