using MoviesWebApi.Shared;

namespace MoviesWebApi.Queries.VratiZanrSve
{
    public sealed record VratiZanrSveQuery() : IQuery<List<ZanrResponse>>;
}
