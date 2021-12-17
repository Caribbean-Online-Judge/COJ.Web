using COJ.Web.Domain.Entities;
using COJ.Web.Domain.Values;
using COJ.Web.Infrastructure.MediatR.Commands;
using COJ.Web.Infrestructure.Data;

using MediatR;

namespace COJ.Web.Infrastructure.MediatR.Handlers
{
    public sealed class CreateAccountTokenCommandHandler : IRequestHandler<CreateAccountTokenCommand, AccountToken>
    {
        private readonly MainDbContext db;

        public CreateAccountTokenCommandHandler(MainDbContext db)
        {
            this.db = db;
        }

        public async Task<AccountToken> Handle(CreateAccountTokenCommand request, CancellationToken cancellationToken)
        {
            return await Task.Run(() =>
            {
                try
                {
                    var newAccountToken = new AccountToken
                    {
                        Account = request.Account,
                        Token = request.Token,
                        ExpirationTime = request.ExpirationTime,
                        Type = request.Type,
                    };

                    db.AccountTokens.Add(newAccountToken);
                    db.SaveChanges();

                    return newAccountToken;
                }
                catch (Exception ex)
                {
                    throw;
                }
            });
        }
    }
}
