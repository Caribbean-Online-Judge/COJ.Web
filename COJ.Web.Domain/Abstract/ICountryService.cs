using System.Collections;
using COJ.Web.Domain.MediatR;

namespace COJ.Web.Domain.Abstract;

public interface ICountryService
{
    IQueryable GetAll(bool isForPublic = true);
}