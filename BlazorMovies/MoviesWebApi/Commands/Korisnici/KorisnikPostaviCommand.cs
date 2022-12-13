using MoviesWebApi.Models;
using MoviesWebApi.Shared;
using MoviesWebApi.ViewModels;

namespace MoviesWebApi.Commands.Korisnici

{
    public sealed record KorisnikPostaviCommand(
            int Id,
            int KorisnikId,
            string EMail,
            string Ime,
            string Prezime,
            decimal Potroseno
        ) : ICommand;

}
