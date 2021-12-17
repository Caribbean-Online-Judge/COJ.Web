namespace COJ.Web.Domain.Models;

public class PaginatedResult<T> where T : class
{
    public PaginatedResult()
    {
        Items = new List<T>();
    }

    public IList<T> Items { get; set; }



    public int Page { get; set; }
    public int Count { get; set; }
    public int Total { get; set; }
}

