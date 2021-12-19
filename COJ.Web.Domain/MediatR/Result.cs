namespace COJ.Web.Domain.MediatR;

public class Result<T>
{
    public Result()
    {
    }

    public Result(T value)
    {
        Value = value;
    }

    public Result(Exception e)
    {
        HasError = true;
        Exception = e;
    }

    public T Value { get; set; }
    public bool HasError { get; set; }
    public Exception Exception { get; set; }
}