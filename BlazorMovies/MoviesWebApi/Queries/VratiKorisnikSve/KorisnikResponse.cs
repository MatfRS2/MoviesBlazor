using MoviesWebApi.Shared;

namespace MoviesWebApi.Queries.VratiKorisnikSve
{    
    public sealed record KorisnikResponse(int KorisnikId, string eMail, string ime, string prezime, 
        decimal potorseno);
}
