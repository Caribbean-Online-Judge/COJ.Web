using COJ.Web.Domain;
using COJ.Web.Domain.Abstract;
using COJ.Web.Domain.Entities;
using COJ.Web.Domain.MediatR;
using COJ.Web.Domain.Models;
using COJ.Web.Domain.Values;
using COJ.Web.Infrestructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;

namespace COJ.Web.Infrastructure.MediatR.Commands;

public class SignInAccountCommand : SignInModel, IRequest<Result<Account>>
{
    public string IpAddress { get; set; } = string.Empty;
}

public sealed class SignInAccountCommandHandler : IRequestHandler<SignInAccountCommand, Result<Account>>
{
    private readonly MainDbContext db;

    public SignInAccountCommandHandler(MainDbContext db, IHashService hashService)
    {
        this.db = db;
        HashService = hashService;
    }

    public IHashService HashService { get; }

    public async Task<Result<Account>> Handle(SignInAccountCommand request, CancellationToken cancellationToken)
    {
        try
        {
            Account? account = null;

            var accountsWithIncludes = db.Accounts.Include(x => x.Permissions);

            account = MailAddress.TryCreate(request.UsernameOrEmail, out var emailAddress)
                ? accountsWithIncludes.SingleOrDefault(x => x.Email == request.UsernameOrEmail && x.EmailConfirmed && x.Enabled)
                : accountsWithIncludes.SingleOrDefault(x => x.Username == request.UsernameOrEmail && x.EmailConfirmed && x.Enabled);

            if (account != null)
            {
                var result = HashService.VerifyHash(account.Password, request.Password);
                if (result == HashVerificationResult.Success)
                {
                    account.LastConnectionDate = DateTime.UtcNow;
                    account.LastIpAddress = request.IpAddress;

                    await db.SaveChangesAsync(cancellationToken);

                    return new Result<Account>(account);
                }
                else
                {
                    return new Result<Account>(new UnauthorizedAccessException());
                }
            }
            else
                return new Result<Account>(new DisabledAccountException());
        }
        catch (Exception e)
        {
            return new Result<Account>(e);
        }
    }
}
