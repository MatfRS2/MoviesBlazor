using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MoviesWebApi.Shared
{
    public abstract class ApiController:ControllerBase
    {
        protected readonly ISender _sender;

        protected ApiController(ISender sender)
        {
            _sender = sender ?? throw new ArgumentNullException(nameof(sender));
        }
    }
}
