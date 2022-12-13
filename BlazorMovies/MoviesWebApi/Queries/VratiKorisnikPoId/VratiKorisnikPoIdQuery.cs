using MoviesWebApi.Shared;

namespace MoviesWebApi.Queries.VratiKorisnikPoId
{
    public sealed record VratiKorisnikPoIdQuery(int KorisnikId) : IQuery<KorisnikResponse>;
}
