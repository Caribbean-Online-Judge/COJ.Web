using COJ.Web.Domain.Entities;
using COJ.Web.Domain.MediatR;
using COJ.Web.Domain.Models;
using COJ.Web.Domain.Values;

namespace COJ.Web.Domain.Abstract;

public interface IProblemService
{
    public Task<PaginatedResult<object>> GetPaginatedProblems(PaginationArguments arguments);

    public Task<Problem> CreateProblem(CreateProblemRequest request);
    Task<bool> DeleteProblemById(int id);
    Task<Result<ProblemFeatures>> GetProblemDetailsById(int id, string locale = Locales.DefaultLocale);
}