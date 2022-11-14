using MoviesWebApi.Shared;

namespace MoviesWebApi.Queries.VratiSveZanrove
{
    public sealed record VratiSveZanroveQuery() : IQuery<List<ZanrJedanResponse>>;
}
