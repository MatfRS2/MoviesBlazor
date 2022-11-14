using MediatR;

namespace MoviesWebApi.Shared
{
    public interface IQueryHandler<TQuery, TResponse>
        : IRequestHandler<TQuery, Result<TResponse>>
        where TQuery: IQuery<TResponse>
    {
    }
}
