
using MoviesWebApi.Models;

namespace MoviesWebApi.ViewModels
{
    public class FilmPostDto
    {
        public string Naslov { get; set; }

        public DateTime DatumPocetkaPrikazivanja { get; set; }

        public decimal Ulozeno { get; set; }

        public int ZanrId { get; set; }
    }
}
