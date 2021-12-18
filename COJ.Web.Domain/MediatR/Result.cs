namespace COJ.Web.Domain.MediatR;

public class Result<T>
{
    public T Value { get; set; }
    public bool HasError { get; set; }
    public Exception Exception { get; set; }
}