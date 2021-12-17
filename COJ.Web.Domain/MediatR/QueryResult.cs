namespace COJ.Web.Domain.MediatR;

public class QueryResult<T>
{
    public T Result { get; set; }
    public bool HasError { get; set; }
    public Exception Exception { get; set; }
}