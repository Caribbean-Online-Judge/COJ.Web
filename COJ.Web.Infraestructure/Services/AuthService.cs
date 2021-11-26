using COJ.Web.Domain.Abstract;
using COJ.Web.Domain.Entities;
using COJ.Web.Domain.Models;
using COJ.Web.Domain.Values;
using COJ.Web.Infraestructure.Exceptions;
using COJ.Web.Infraestructure.MediatR.Commands;
using COJ.Web.Infraestructure.MediatR.Queries;

using MediatR;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COJ.Web.Infraestructure.Services;

public sealed class AuthService : IAuthService
{
    private readonly IMediator _mediator;
    private readonly IHashService hashService;
    private readonly IEmailService emailService;

    public AuthService(IMediator mediator, IHashService hashService, IEmailService emailService)
    {
        _mediator = mediator;
        this.hashService = hashService;
        this.emailService = emailService;
    }

    public Account RecoverAccount(string emailOrUsername)
    {
        throw new NotImplementedException();
    }

    public Account SignIn(Account account)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    /// <exception cref="AccountEmailUsedException">If the provided email is used</exception>
    public async Task<Account> SignUp(SignUpModel request)
    {
        var isUsedAccountEmail = await _mediator.Send(new IsUsedAccountEmailQuery
        {
            Email = request.Email
        });

        if (isUsedAccountEmail)
            throw new AccountEmailUsedException();

        var passwordHashed = hashService.ComputeHash(request.Password);

        var command = new CreateAccountCommand()
        {
            FirstName = request.FirstName,
            Birthday = request.Birthday,
            CountryId = request.CountryId,
            Email = request.Email,
            InstitutionId = request.InstitutionId,
            LanguageId = request.LanguageId,
            LastName = request.LastName,
            LocaleId = request.LocaleId,
            Nick = request.Nick,
            Password = passwordHashed,
            Username = request.Username,
            Sex = request.Sex,
        };
        var createdAccount = await _mediator.Send(command);

        var accountToken = await CreateAccountConfirmationToken(createdAccount);

        var emailResult = await emailService.SendAccountConfirmation(createdAccount.Email, accountToken.Token);

        return createdAccount;
    }

    private async Task<AccountToken> CreateAccountConfirmationToken(Account newAccount)
    {
        var accountToken = await _mediator.Send(new CreateAccountTokenCommand()
        {
            Account = newAccount,
            ExpirationTime = TimeSpan.FromHours(2),
            Token = Guid.NewGuid().ToString(),
            Type = AccountTokenType.EmailConfirmation
        });

        return accountToken;
    }
}

