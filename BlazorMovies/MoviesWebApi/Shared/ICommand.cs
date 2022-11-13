using MediatR;

namespace MoviesWebApi.Shared
{
    public interface ICommand : IRequest<Result>
    {
    }

    public interface ICommand<TResponse> : IRequest<Result<TResponse>>
    {
    }
}
