using MoviesWebApi.Shared;
using MoviesWebApi.ViewModels;

namespace MoviesWebApi.Operations
{
    public interface IZanroviOperations
    {
        public Task<Result<List<ZanrDto>>> SviZanrovi();

        public Task<Result<ZanrDto>> ZanrPoId(int id);

        public Task<Result> PostaviZanr(int id, ZanrDto zanrDto);

        public Task<Result> DodajZanr(ZanrDto zanrDto);

        public Task<Result> ObrisiZanrPoId(int id);

    }
}
