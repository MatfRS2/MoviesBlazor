using MoviesWebApi.Models;

namespace MoviesWebApi.ViewModels
{
    public class PretplataGetDto 
    {
        public int PretplataId { get; set; }

        public PretplataStatus Status { get; set; }

        public decimal Iznos { get; set; }

        public DateTime DatumIsteka { get; set; }

        public int KorisnikId { get; set; }
        public KorisnikGetDto Korisnik { get; set; }
        
        public int PaketId { get; set; }
        public PaketGetDto Paket { get; set; }        
    }
}