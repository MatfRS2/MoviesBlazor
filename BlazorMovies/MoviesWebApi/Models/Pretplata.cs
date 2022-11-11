namespace MoviesWebApi.Models
{
    public class Pretplata 
    {
        public int PretplataId { get; set; }

        public PretplataStatus Status { get; set; }

        public decimal Iznos { get; set; }

        public DateTime DatumIsteka { get; set; }

        public int KorisnikId { get; set; }
        public Korisnik Korisnik { get; set; }
        
        public int PaketId { get; set; }
        public Paket Paket { get; set; }        
    }
}