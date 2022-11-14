using MoviesWebApi.Models;
using MoviesWebApi.Shared;
using MoviesWebApi.ViewModels;

namespace MoviesWebApi.Commands.Zanrovi
{
    public sealed record ObrisiZanrCommand(
            int Id
        ) : ICommand;

}
