namespace COJ.Web.API.Models.QueryParams
{
    public sealed class GetAllProblemsQueryParameters : PaginatedQueryParams
    {
        public string SearchBy { get; set; } = string.Empty;
        public string[]? OrderBy { get; set; } = null;
    }
}
