using MoviesWebApi.Models;

namespace MoviesWebApi.ViewModels
{
    public class FilmPaketGetDto
    {
        public int FilmId { get; set; }
        public FilmGetDto Film { get; set; }

        public int PaketId { get; set; }
        public PaketGetDto Paket { get; set; }
    }
}
