using MoviesWebApi.ViewModels;

namespace MoviesWebApi.Operations
{
    public interface IZanroviOperations
    {
        public Task<List<ZanrDto>> SviZanrovi();

        public Task<ZanrDto> ZanrPoId(int id);

        public Task<int> PostaviZanr(int id, ZanrDto zanrDto);

        public Task DodajZanr(ZanrDto zanrDto);

        public Task<int> ObrisiZanrPoId(int id);

    }
}
