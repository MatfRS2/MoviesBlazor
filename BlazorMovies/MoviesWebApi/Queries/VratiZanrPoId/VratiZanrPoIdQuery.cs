using MoviesWebApi.Shared;

namespace MoviesWebApi.Queries.VratiZanrPoId
{
    public sealed record VratiZanrPoIdQuery(int ZanrId) : IQuery<KorisnikResponse>;
}
