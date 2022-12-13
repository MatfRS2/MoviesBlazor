using MoviesWebApi.Models;
using MoviesWebApi.Shared;
using MoviesWebApi.ViewModels;

namespace MoviesWebApi.Commands.Korisnici
{
    public sealed record KorisnikDodajCommand(
            int KorisnikId,
            string EMail,
            string Ime,
            string Prezime,
            decimal Potroseno
        ) : ICommand;

}
