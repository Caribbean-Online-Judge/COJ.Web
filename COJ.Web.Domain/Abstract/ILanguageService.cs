using COJ.Web.Domain.Entities;
using COJ.Web.Domain.MediatR;

namespace COJ.Web.Domain.Abstract;

public interface ILanguageService
{
    Task<Result<IEnumerable<Language>>> GetAll();
}