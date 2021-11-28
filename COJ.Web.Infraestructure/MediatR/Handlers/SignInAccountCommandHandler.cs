using COJ.Web.Domain.Abstract;
using COJ.Web.Domain.Entities;
using COJ.Web.Domain.Values;
using COJ.Web.Infraestructure.MediatR.Commands;
using COJ.Web.Infraestructure.MediatR.Queries;
using COJ.Web.Infrestructure.Data;

using MediatR;

using Microsoft.EntityFrameworkCore;

using System.Net.Mail;

namespace COJ.Web.Infraestructure.MediatR.Handlers;

public sealed class SignInAccountCommandHandler : IRequestHandler<SignInAccountCommand, Account?>
{
    private readonly MainDbContext db;

    public SignInAccountCommandHandler(MainDbContext db, IHashService hashService)
    {
        this.db = db;
        HashService = hashService;
    }

    public IHashService HashService { get; }

    public async Task<Account?> Handle(SignInAccountCommand request, CancellationToken cancellationToken)
    {
        Account? account = null;

        var accountsWithIncludes = db.Accounts.Include(x => x.Permissions);

        if (MailAddress.TryCreate(request.UsernameOrEmail, out var emailAddress))
            account = accountsWithIncludes.SingleOrDefault(x => x.Email == request.UsernameOrEmail);
        else
            account = accountsWithIncludes.SingleOrDefault(x => x.Username == request.UsernameOrEmail);

        if (account != null && HashService.VerifyHash(account.Password, request.Password) == HashVerificationResult.Success)
        {
            account.LastConnectionDate = DateTime.UtcNow;
            account.LastIpAddress = request.IpAddress;

            await db.SaveChangesAsync(cancellationToken);

            return account;
        }

        return null;
    }
}

