using COJ.Web.Domain.Entities;
using COJ.Web.Infrastructure.MediatR.Queries;
using COJ.Web.Infrestructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace COJ.Web.Infrastructure.MediatR.Handlers;

public class GetAccountByIdQueryHandler : IRequestHandler<GetAccountByIdQuery, Account?>
{
    private readonly MainDbContext _db;

    public GetAccountByIdQueryHandler(MainDbContext db)
    {
        _db = db;
    }

    public async Task<Account?> Handle(GetAccountByIdQuery request, CancellationToken cancellationToken)
    {
        return await Task.Run(() => _db.Accounts
                .Include(x=> x.Country)
                .Include(x=> x.Institution)
                .Include(x => x.Language)
                .Include(x => x.Locale)
                .Include(x => x.Permissions)
                .Include(x => x.Settings)
                .SingleOrDefault(account => account.Id == request.Id),
            cancellationToken);
    }
}