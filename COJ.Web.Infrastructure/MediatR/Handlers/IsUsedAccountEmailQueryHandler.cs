using COJ.Web.Domain.Entities;
using COJ.Web.Domain.Values;
using COJ.Web.Infrastructure.MediatR.Commands;
using COJ.Web.Infrastructure.MediatR.Queries;
using COJ.Web.Infrestructure.Data;

using MediatR;

namespace COJ.Web.Infrastructure.MediatR.Handlers;

public sealed class IsUsedAcountEmailQueryHandler : IRequestHandler<IsUsedAccountEmailQuery, bool>
{
    private readonly MainDbContext db;

    public IsUsedAcountEmailQueryHandler(MainDbContext db)
    {
        this.db = db;
    }

    public async Task<bool> Handle(IsUsedAccountEmailQuery request, CancellationToken cancellationToken)
    {
        return await Task.Run(() =>
        {
            try
            {
                return db.Accounts.SingleOrDefault(x => x.Email == request.Email) != null;

            }
            catch (Exception ex)
            {
                throw;
            }
        });
    }
}

