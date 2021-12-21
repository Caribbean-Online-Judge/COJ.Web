using System.Collections;
using COJ.Web.Domain.Entities;
using COJ.Web.Domain.MediatR;
using COJ.Web.Domain.Models.Dtos;

namespace COJ.Web.Domain.Abstract;

public interface ILocaleService
{
    public Task<Result<IEnumerable>> GetAll(bool isForPublic = true);
}