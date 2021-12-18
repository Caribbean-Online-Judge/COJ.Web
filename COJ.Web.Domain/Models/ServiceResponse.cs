namespace COJ.Web.Domain.Models;

public class ServiceResponse<T>
{
    public T Value { get; set; }
    public string Message { get; set; }
    public bool HasError { get; set; }
}