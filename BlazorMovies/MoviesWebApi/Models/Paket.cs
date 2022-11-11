namespace MoviesWebApi.Models
{
    public class Paket
    {
        public int PaketId { get; set; }

        public string Naziv { get; set; }

        public string Opis { get; set; }

        public DateTime DatumFormiranja { get; set; }

        public ICollection<FilmPaket> FilmPaketi { get; set; }

        public ICollection<Film> Filmovi { get; set; }

    }
}
