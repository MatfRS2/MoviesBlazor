using Microsoft.Extensions.Hosting;

namespace MoviesWebApi.Models
{
    public class FilmPaket
    {
        public int FilmId { get; set; }
        public Film Film { get; set; }

        public int PaketId { get; set; }
        public Paket Paket { get; set; }
    }
}
