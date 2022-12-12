using MoviesWebApi.Shared;

namespace MoviesWebApi.Queries.VratiKorisnikSve
{
    public sealed record VratiKorisnikSveQuery() : IQuery<List<KorisnikResponse>>;
}
