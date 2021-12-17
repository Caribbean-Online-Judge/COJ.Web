using COJ.Web.Domain.Entities;
using COJ.Web.Infrastructure.MediatR.Commands;
using COJ.Web.Infrestructure.Data;

using MediatR;
using Microsoft.EntityFrameworkCore;

namespace COJ.Web.Infrastructure.MediatR.Handlers;

public sealed class AddRefreshTokenCommandHandler : IRequestHandler<AddRefreshTokenCommand>
{
    private readonly MainDbContext _db;

    public AddRefreshTokenCommandHandler(MainDbContext db)
    {
        _db = db;
    }

    public async Task<Unit> Handle(AddRefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var accountWithTokens = _db.Accounts.Include(x => x.RefreshTokens)
            .SingleOrDefault(x => x.Id == request.Account.Id) ?? default;

        accountWithTokens.RefreshTokens.Add(request.RefreshToken);

        await _db.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }

}

