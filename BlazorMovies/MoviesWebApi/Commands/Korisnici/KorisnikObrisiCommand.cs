using MoviesWebApi.Models;
using MoviesWebApi.Shared;
using MoviesWebApi.ViewModels;

namespace MoviesWebApi.Commands.Korisnici
{
    public sealed record KorisnikObrisiCommand(
            int Id
        ) : ICommand;

}
