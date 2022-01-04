using COJ.Web.Domain.Models;
using COJ.Web.Infrastructure.Utils;
using FluentValidation;

namespace COJ.Web.Infrastructure.Validators;

public class SignUpRequestValidator: AbstractValidator<SignUpRequest> 
{
    public SignUpRequestValidator()
    {
        RuleFor(x => x.Password)
            .NotNull()
            .NotEmpty()
            .Matches(Validator.PasswordRegex);
        RuleFor(x => x.Email)
            .NotNull()
            .NotEmpty()
            .EmailAddress();
        RuleFor(x => x.Username)
            .MinimumLength(5)
            .MaximumLength(18);
        RuleFor(x => x.LanguageId)
            .NotNull()
            .GreaterThan(0);
        RuleFor(x => x.LocaleId)
            .NotNull()
            .GreaterThan(0);
        RuleFor(x => x.CountryId)
            .NotNull()
            .GreaterThan(0);
        RuleFor(x => x.InstitutionId)
            .NotNull()
            .GreaterThan(0);
        RuleFor(x => x.FirstName)
            .NotNull()
            .NotEmpty();
        RuleFor(x => x.LastName)
            .NotNull()
            .NotEmpty();
        RuleFor(x => x.Birthday)
            .LessThan(DateTime.UtcNow.AddYears(12));
        RuleFor(x => x.Sex)
            .NotNull();
    }
}