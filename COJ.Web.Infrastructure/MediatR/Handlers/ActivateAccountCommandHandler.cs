using COJ.Web.Domain.Entities;
using COJ.Web.Domain.MediatR;
using COJ.Web.Domain.Values;
using COJ.Web.Infrastructure.MediatR.Commands;
using COJ.Web.Infrestructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace COJ.Web.Infrastructure.MediatR.Handlers;

public sealed class ActivateAccountCommandHandler : IRequestHandler<ActivateAccountCommand, Result<Unit>>
{
    private readonly MainDbContext _db;

    public ActivateAccountCommandHandler(MainDbContext db)
    {
        _db = db;
    }

    public async Task<Result<Unit>> Handle(ActivateAccountCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var token = _db.AccountTokens
                .Include(x => x.Account)
                .SingleOrDefault(x =>
                    x.Token == request.ActivationToken && x.Type == AccountTokenType.EmailConfirmation
                );

            if (token == null)
                throw new NullReferenceException();

            if (token.ExpirationTime < DateTime.UtcNow)
                throw new SecurityTokenExpiredException();

            if (token.Account.Email != request.Email)
                throw new InvalidOperationException();

            token.Account.EmailConfirmed = true;
            token.Account.Enabled = true;

            await _db.SaveChangesAsync(cancellationToken);

            _db.AccountTokens.Remove(token);

            await _db.SaveChangesAsync(cancellationToken);

            return new Result<Unit>();
        }
        catch (Exception e)
        {
            return new Result<Unit>
            {
                HasError = true,
                Exception = e
            };
        }
    }
}