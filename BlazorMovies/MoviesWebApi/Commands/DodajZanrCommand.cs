using MoviesWebApi.Models;
using MoviesWebApi.Shared;
using MoviesWebApi.ViewModels;

namespace MoviesWebApi.Commands
{
    public sealed record DodajZanrCommand(
            int ZanrId,
            string ZanrNaziv
        ) : ICommand;

}
