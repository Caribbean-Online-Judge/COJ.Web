using COJ.Web.Domain.Abstract;
using COJ.Web.Domain.Entities;
using COJ.Web.Domain.MediatR;
using COJ.Web.Infrastructure.MediatR.Queries;
using MediatR;

namespace COJ.Web.Infrastructure.Services;

public class LanguageService : ILanguageService
{
    private readonly IMediator _mediator;

    public LanguageService(IMediator _mediator)
    {
        this._mediator = _mediator;
    }
    public async Task<Result<IEnumerable<Language>>> GetAll()
    {
        var result = await _mediator.Send(new GetAllLanguagesQuery());
        return result;
    }
}