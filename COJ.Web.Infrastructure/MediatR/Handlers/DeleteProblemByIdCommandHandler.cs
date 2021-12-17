using COJ.Web.Domain.Entities;
using COJ.Web.Domain.Values;
using COJ.Web.Infrastructure.Extensions;
using COJ.Web.Infrastructure.MediatR.Commands;
using COJ.Web.Infrestructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace COJ.Web.Infrastructure.MediatR.Handlers
{
    public sealed class DeleteProblemByIdCommandHandler : IRequestHandler<DeleteProblemByIdCommand, bool>
    {
        private readonly MainDbContext _db;

        public DeleteProblemByIdCommandHandler(MainDbContext db)
        {
            _db = db;
        }

        public async Task<bool> Handle(DeleteProblemByIdCommand request, CancellationToken cancellationToken)
        {
            var problem = request.DeleteRelatedData
                ? _db.Problems
                    //Here we should include others data related that should be deleted
                    .Include(x => x.DataSets)
                    .Include(x => x.ProblemStatistics)
                    .SingleOrDefault(x => x.Id == request.Id)
                : _db.Problems.SingleOrDefault(x => x.Id == request.Id);

            if (problem == null)
                return false;

            _db.Problems.Remove(problem);
            _db.ProblemDataSets.RemoveRange(problem.DataSets);
            _db.ProblemStatistics.Remove(problem.ProblemStatistics);

            await _db.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}