using COJ.Web.Domain.Entities;
using COJ.Web.Domain.MediatR;
using COJ.Web.Domain.Models;
using COJ.Web.Domain.Values;

namespace COJ.Web.Domain.Abstract;

public interface IProblemService
{
    Task<PaginatedResult<object>> GetPaginatedProblems(int page, int pageSize, string searchBy = "", string[]? orderBy = null);

    Task<Problem> CreateProblem(CreateProblemRequest request);
    Task<bool> DeleteProblemById(int id);
    Task<Result<ProblemFeatures>> GetProblemDetailsById(int id, string locale = Locales.DefaultLocale);
    Task<Result<bool>> AddNewSubmission(int requestProblemId);
    Task<Result<ProblemStatistic>> GetProblemStatistics(int problemId);
}