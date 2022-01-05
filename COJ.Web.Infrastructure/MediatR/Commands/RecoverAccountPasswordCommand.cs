using COJ.Web.Domain.Abstract;
using COJ.Web.Domain.MediatR;
using COJ.Web.Domain.Values;
using COJ.Web.Infrestructure.Data;
using MediatR;

namespace COJ.Web.Infrastructure.MediatR.Commands;

public class RecoverAccountPasswordCommand : IRequest<Result<string>>
{
    public string Email { get; set; }
}

public sealed class
    RecoverAccountPasswordCommandHandler : IRequestHandler<RecoverAccountPasswordCommand,
        Result<string>>
{
    private readonly MainDbContext _db;
    private readonly ITokenService _tokenService;

    public RecoverAccountPasswordCommandHandler(MainDbContext db, ITokenService tokenService)
    {
        _db = db;
        _tokenService = tokenService;
    }


    public async Task<Result<string>> Handle(RecoverAccountPasswordCommand request, CancellationToken cancellationToken)
    {
        var transaction = await _db.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            var account = _db.Accounts.Single(x => x.Email == request.Email);
            var recoverPasswordToken = _tokenService.GenerateAccountToken(AccountTokenType.PasswordReset);
            recoverPasswordToken.Account = account;
            _db.AccountTokens.Add(recoverPasswordToken);
            await _db.SaveChangesAsync(cancellationToken);

            await transaction.CommitAsync(cancellationToken);
            return new Result<string>(recoverPasswordToken.Token);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            return new Result<string>(ex);
        }
    }
}