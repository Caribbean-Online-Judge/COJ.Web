using COJ.Web.Domain.Abstract;
using COJ.Web.Domain.Entities;
using COJ.Web.Domain.Models;
using COJ.Web.Infrastructure.MediatR.Commands;
using COJ.Web.Infrastructure.MediatR.Queries;
using MediatR;
using Microsoft.IdentityModel.Tokens;

namespace COJ.Web.Infrastructure.Services;

public class AccountService : IAccountService
{
    private readonly IMediator _mediator;

    public AccountService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<ServiceResponse<bool>> ConfirmAccount(ConfirmAccountRequest request)
    {
        var result = await _mediator.Send(new ActivateAccountCommand
        {
            Email = request.Email,
            ActivationToken = request.Token
        });

        var message = result.Exception switch
        {
            NullReferenceException => "Token not found",
            SecurityTokenExpiredException => "The token was expired",
            InvalidOperationException => "The token and email don't match",
            _ => string.Empty
        };

        return new ServiceResponse<bool>
        {
            Message = message,
            Value = !result.HasError,
            HasError = result.HasError
        };
    }

    public async Task<Account> GetAccountById(int userId)
    {
        return await _mediator.Send(new GetAccountByIdQuery
        {
            Id = userId
        });
    }
}