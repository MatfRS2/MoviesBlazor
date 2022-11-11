namespace MoviesWebApi.Models
{
    public class Film
    {
        public int FilmId { get; set; }
        
        public string Naslov { get; set; }

        public DateTime DatumPocetkaPrikazivanja { get; set; }
        
        public decimal Ulozeno { get; set; }

        public Zanr Zanr { get; set; }

        public int ZanrId { get; set; }

        public ICollection<FilmPaket> FilmPaketi { get; set; }
        public ICollection<Paket> Paketi { get; set; }
    }
}
