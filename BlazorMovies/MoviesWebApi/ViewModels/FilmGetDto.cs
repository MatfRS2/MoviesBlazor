
using MoviesWebApi.Models;

namespace MoviesWebApi.ViewModels
{
    public class FilmGetDto
    {
        public int FilmId { get; set; }

        public string Naslov { get; set; }

        public DateTime DatumPocetkaPrikazivanja { get; set; }

        public decimal Ulozeno { get; set; }

        public ZanrDto Zanr { get; set; }

        public int ZanrId { get; set; }
    }
}
