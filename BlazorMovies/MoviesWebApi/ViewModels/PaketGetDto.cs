using MoviesWebApi.Models;

namespace MoviesWebApi.ViewModels
{
    public class PaketGetDto
    {
        public int PaketId { get; set; }

        public string Naziv { get; set; }

        public string Opis { get; set; }

        public DateTime DatumFormiranja { get; set; }
    }
}
