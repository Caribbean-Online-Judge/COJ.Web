using COJ.Web.Domain.Entities;
using COJ.Web.Domain.MediatR;
using COJ.Web.Domain.Values;
using COJ.Web.Infrastructure.MediatR.Commands;
using COJ.Web.Infrestructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace COJ.Web.Infrastructure.MediatR.Handlers;

public sealed class CreateSubmissionCommandHandler : IRequestHandler<CreateSubmissionCommand, Result<ProblemSubmission>>
{
    private readonly MainDbContext _db;

    public CreateSubmissionCommandHandler(MainDbContext db)
    {
        _db = db;
    }


    public async Task<Result<ProblemSubmission>> Handle(CreateSubmissionCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            var problem = _db.Problems
                .SingleOrDefault(x => x.Id == request.ProblemId);
            var account = _db.Accounts
                .SingleOrDefault(x => x.Id == request.AccountId);
            var language = _db.Languages
                .SingleOrDefault(x => x.Id == request.LanguageId);


            if (problem == null || account == null || language == null)
                throw new NullReferenceException();

            var problemSubmission = new ProblemSubmission
            {
                Account = account,
                Language = language,
                Problem = problem,
                SourceCode = request.SourceCode,
                Status = SubmissionStatus.Created
            };

            _db.ProblemSubmissions.Add(problemSubmission);

            await _db.SaveChangesAsync(cancellationToken);

            return new Result<ProblemSubmission>(problemSubmission);
        }
        catch (Exception e)
        {
            return new Result<ProblemSubmission>(e);
        }
    }
}