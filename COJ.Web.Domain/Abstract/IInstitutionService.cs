using System.Collections;
using COJ.Web.Domain.Entities;
using COJ.Web.Domain.MediatR;

namespace COJ.Web.Domain.Abstract;

public interface IInstitutionService
{
    Task<Result<IEnumerable>> GetAll(int? countryId, bool isForPublic = true);
}