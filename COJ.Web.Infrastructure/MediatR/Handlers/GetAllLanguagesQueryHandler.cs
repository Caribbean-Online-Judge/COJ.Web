using COJ.Web.Domain.Entities;
using COJ.Web.Domain.MediatR;
using COJ.Web.Domain.Values;
using COJ.Web.Infrastructure.MediatR.Commands;
using COJ.Web.Infrastructure.MediatR.Queries;
using COJ.Web.Infrestructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace COJ.Web.Infrastructure.MediatR.Handlers;

public sealed class GetAllLanguagesQueryHandler : IRequestHandler<GetAllLanguagesQuery, Result<IEnumerable<Language>>>
{
    private readonly MainDbContext _db;

    public GetAllLanguagesQueryHandler(MainDbContext db)
    {
        _db = db;
    }

    public async Task<Result<IEnumerable<Language>>> Handle(GetAllLanguagesQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var locales = await _db.Languages
                .ToListAsync(cancellationToken: cancellationToken);
            return new Result<IEnumerable<Language>>(locales);
        }
        catch (Exception e)
        {
            return new Result<IEnumerable<Language>>
            {
                HasError = true,
                Exception = e
            };
        }
    }
}