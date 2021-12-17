using COJ.Web.Domain.Abstract;
using COJ.Web.Domain.Entities;
using COJ.Web.Domain.Models;
using COJ.Web.Infrastructure.MediatR.Commands;
using COJ.Web.Infrestructure.Data;

using MediatR;
using Microsoft.EntityFrameworkCore;

namespace COJ.Web.Infrastructure.MediatR.Handlers;

public sealed class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, RefreshTokenResult?>
{
    private readonly MainDbContext _db;
    private readonly ITokenService _tokenService;
    private readonly IJwtService _jwtService;

    public RefreshTokenCommandHandler(MainDbContext db, ITokenService tokenService, IJwtService jwtService)
    {
        _db = db;
        _tokenService = tokenService;
        _jwtService = jwtService;
    }

    public async Task<RefreshTokenResult?> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var user = _db.Accounts
            .Include(x => x.RefreshTokens)
            .SingleOrDefault(x => x.RefreshTokens.Any(y => y.Token == request.RefreshToken));
        // return null if no user found with token
        if (user == null) return null;

        var oldToken = user.RefreshTokens.Single(x => x.Token == request.RefreshToken);

        // return null if token is no longer active
        if (oldToken.IsExpired) return null;

        // replace old refresh token with a new one and save
        var newToken = _tokenService.GenerateRefreshToken();
        oldToken.Created = DateTime.UtcNow;
        oldToken.Expires = DateTime.UtcNow.AddDays(1);
        oldToken.Token = newToken.Token;

        await _db.SaveChangesAsync(cancellationToken);

        var jwtToken = _jwtService.ComputeToken(user, out var duration);
        return new RefreshTokenResult()
        {
            Token = jwtToken,
            Duration = duration,
            RefreshToken = newToken.Token
        };
    }

}

