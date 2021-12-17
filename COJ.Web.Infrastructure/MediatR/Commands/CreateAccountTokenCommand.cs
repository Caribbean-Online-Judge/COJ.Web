using COJ.Web.Domain.Entities;
using COJ.Web.Domain.Models;
using COJ.Web.Domain.Values;
using COJ.Web.Infrestructure.Data;
using MediatR;

namespace COJ.Web.Infrastructure.MediatR.Commands;

public class CreateAccountTokenCommand : IRequest<AccountToken>
{
    public string Token { get; set; }
    public Account Account { get; set; }
    public TimeSpan ExpirationTime { get; set; }
    public AccountTokenType Type { get; set; }
}