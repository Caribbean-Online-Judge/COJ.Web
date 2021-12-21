using System.Collections;
using COJ.Web.Domain.MediatR;

namespace COJ.Web.Domain.Abstract;

public interface ICountryService
{
    Task<Result<IEnumerable>> GetAll(bool isForPublic = true);
}