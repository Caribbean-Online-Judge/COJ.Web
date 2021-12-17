using MediatR;

namespace COJ.Web.Domain.MediatR;

public interface ILocalizedRequest<out T> : IRequest<T>
{
    public string Locale { get; set; }
}