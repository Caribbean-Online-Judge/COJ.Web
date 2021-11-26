using COJ.Web.Domain.Entities;
using COJ.Web.Domain.Values;
using COJ.Web.Infraestructure.MediatR.Commands;
using COJ.Web.Infrestructure.Data;

using MediatR;

namespace COJ.Web.Infraestructure.MediatR.Handlers
{
    public sealed class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, Account>
    {
        private readonly MainDbContext db;

        public CreateAccountCommandHandler(MainDbContext db)
        {
            this.db = db;
        }

        public async Task<Account> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            return await Task.Run(() =>
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
                        Nick = request.Nick,
                        Password = request.Password,
                        Username = request.Username,
                        Sex = request.Sex,
                        Country = country,
                        Institution = institution,
                        Language = language,
                        Locale = locale,
                        Role = AccountRole.User,
                        Settings = AccountSettings.Default
                    };
                    db.Accounts.Add(newAccount);
                    db.SaveChanges();

                    return newAccount;
                }
                catch (Exception ex)
                {
                    throw;
                }
            });
        }
    }
}
