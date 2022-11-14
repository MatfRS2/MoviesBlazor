using MediatR;
namespace MoviesWebApi.Shared
{
    public interface IQuery<TResponse>: IRequest<Result<TResponse>>
    {
    }
}
