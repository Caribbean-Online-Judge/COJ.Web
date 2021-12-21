using System.Collections;
using COJ.Web.Domain.Abstract;
using COJ.Web.Domain.Entities;
using COJ.Web.Domain.MediatR;
using COJ.Web.Domain.Models.Dtos;
using COJ.Web.Infrastructure.MediatR.Queries;
using MediatR;

namespace COJ.Web.Infrastructure.Services;

public class LocaleService : ILocaleService
{
    private readonly IMediator _mediator;

    public LocaleService(IMediator _mediator)
    {
        this._mediator = _mediator;
    }
    public async Task<Result<IEnumerable>> GetAll(bool isForPublic = true)
    {
        var result = await _mediator.Send(new GetAllLocalesQuery()
        {
            IsForPublic = isForPublic
        });
        return result;
    }
    
}