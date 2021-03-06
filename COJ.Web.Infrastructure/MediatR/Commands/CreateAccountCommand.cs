using COJ.Web.Domain.Entities;
using COJ.Web.Domain.Models;
using COJ.Web.Infrestructure.Data;
using MediatR;

namespace COJ.Web.Infrastructure.MediatR.Commands;

public class CreateAccountCommand : SignUpRequest, IRequest<Account>
{

}

public sealed class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, Account>
{
    private readonly MainDbContext db;

    public CreateAccountCommandHandler(MainDbContext db)
    {
        this.db = db;
    }

    public async Task<Account> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var country = db.Countries.Single(country => country.Id == request.CountryId && country.Enabled);
            var institution = db.Institutions.Single(x => x.Id == request.InstitutionId && x.Enabled);
            var locale = db.Locales.Single(x => x.Id == request.LocaleId);
            var language = db.Languages.Single(x => x.Id == request.LanguageId && x.Enabled);

            var newAccount = new Account()
            {
                FirstName = request.FirstName,
                Birthday = request.Birthday,
                Email = request.Email,
                LastName = request.LastName,
                Password = request.Password,
                Username = request.Username,
                Sex = request.Sex,
                Country = country,
                Institution = institution,
                Language = language,
                Locale = locale,
                Settings = AccountSettings.Default
            };
            await db.Accounts.AddAsync(newAccount, cancellationToken);
            await db.SaveChangesAsync(cancellationToken);

            return newAccount;
        }
        catch (Exception)
        {
            throw;
        }

    }
}