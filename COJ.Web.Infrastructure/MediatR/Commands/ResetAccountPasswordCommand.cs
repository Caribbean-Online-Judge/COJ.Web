using COJ.Web.Domain.MediatR;
using COJ.Web.Domain.Models;
using COJ.Web.Infrestructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace COJ.Web.Infrastructure.MediatR.Commands;

public class ResetAccountPasswordCommand : ResetAccountPasswordRequest, IRequest<Result<bool>>
{
}

public sealed class
    ResetAccountPasswordCommandHandler : IRequestHandler<ResetAccountPasswordCommand,
        Result<bool>>
{
    private readonly MainDbContext _db;

    public ResetAccountPasswordCommandHandler(MainDbContext db)
    {
        _db = db;
    }


    public async Task<Result<bool>> Handle(ResetAccountPasswordCommand request, CancellationToken cancellationToken)
    {
        var transaction = await _db.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            /*
             * Requirements for use a token to reset password
             * - The token should be valid
             * - The token should be active
             * - The token should be associated with the provided email by account association
             * - The account associated should have the email confirmed
             */
            var token = _db.AccountTokens
                .Include(x => x.Account)
                .Single(x =>
                x.Token == request.Token
                && x.ExpirationTime > DateTime.UtcNow
                && x.Account.EmailConfirmed
                && x.Account.Email == request.Email);

            token.Account.Password = request.NewPassword;
            await _db.SaveChangesAsync(cancellationToken);
            
            _db.AccountTokens.Remove(token);
            await _db.SaveChangesAsync(cancellationToken);

            await transaction.CommitAsync(cancellationToken);
            return new Result<bool>();
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            return new Result<bool>(ex);
        }
    }
}