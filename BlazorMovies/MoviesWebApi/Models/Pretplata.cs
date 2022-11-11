namespace MoviesWebApi.Models
{
    public class Pretplata 
    {
        public PretplataStatus Status { get; set; }
        public Korisnik Korisnik { get; set; }
        public Paket Paket { get; set; }
        public decimal Iznos { get; set; }
        public DateTime DatumIsteka { get; set; }
    }
}