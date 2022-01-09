using COJ.Web.Domain.Abstract;
using COJ.Web.Domain.Entities;
using COJ.Web.Domain.Models;
using COJ.Web.Domain.Values;
using COJ.Web.Domain.Exceptions;
using COJ.Web.Infrastructure.MediatR.Commands;
using COJ.Web.Infrastructure.MediatR.Queries;
using MediatR;
using COJ.Web.Domain.Models.Dtos;
using COJ.Web.Domain.MediatR;
using COJ.Web.Domain;

namespace COJ.Web.Infrastructure.Services;

public sealed class AuthService : IAuthService
{
    private readonly IMediator _mediator;
    private readonly IHashService hashService;
    private readonly IEmailService emailService;
    private readonly ITokenService _tokenService;
    private readonly IJwtService _jwtService;

    public AuthService(IMediator mediator, IHashService hashService, IEmailService emailService, IJwtService jwtService, ITokenService tokenService)
    {
        _mediator = mediator;
        this.hashService = hashService;
        this.emailService = emailService;
        _jwtService = jwtService;
        _tokenService = tokenService;
    }

    public Account RecoverAccount(string emailOrUsername)
    {
        throw new NotImplementedException();
    }

    public Task<RefreshTokenResult?> RefreshToken(string token)
    {
        var result = _mediator.Send(new RefreshTokenCommand
        {
            RefreshToken = token
        });

        return result;
    }

    public async Task<Result<SignInResponse>> SignIn(SignInModel request, SignInArguments arguments)
    {
        var accountResult = await _mediator.Send(new SignInAccountCommand()
        {
            UsernameOrEmail = request.UsernameOrEmail,
            Password = request.Password,
            IpAddress = arguments.IpAddress,
        });

        if (accountResult.HasError)
            return new Result<SignInResponse>(accountResult.Exception);

        var token = _jwtService.ComputeToken(accountResult.Value, out var expirationTime);
        var refreshToken = _tokenService.GenerateRefreshToken(arguments.IpAddress);

        await _mediator.Send(new AddRefreshTokenCommand()
        {
            RefreshToken = refreshToken,
            Account = accountResult.Value
        });

        return new Result<SignInResponse>(new SignInResponse(token, refreshToken.Token, expirationTime));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    /// <exception cref="AccountEmailUsedException">If the provided email is used</exception>
    public async Task<Result<Account>> SignUp(SignUpRequest request)
    {
        var isUsedAccountEmail = await _mediator.Send(new IsUsedAccountEmailQuery
        {
            Email = request.Email
        });

        if (isUsedAccountEmail)
            return new Result<Account>(new AccountEmailUsedException());

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
            Password = passwordHashed,
            Username = request.Username,
            Sex = request.Sex,
        };
        var createdAccount = await _mediator.Send(command);

        var accountToken = await CreateAccountConfirmationToken(createdAccount);

        var emailResult = await emailService.SendAccountConfirmation(createdAccount.Email, accountToken.Token);

        return new Result<Account>(createdAccount);
    }

    private async Task<AccountToken> CreateAccountConfirmationToken(Account newAccount)
    {
        var accountToken = await _mediator.Send(new CreateAccountTokenCommand()
        {
            Account = newAccount,
            ExpirationTime = DateTime.UtcNow.AddHours(12),
            Token = Guid.NewGuid().ToString(),
            Type = AccountTokenType.EmailConfirmation
        });

        return accountToken;
    }
}

