using MoviesWebApi.Models;

namespace MoviesWebApi.ViewModels
{
    public class KorisnikGetDto
    {
        public int KorisnikId { get; set; }
        
        public string Email { get; set;}
        
        public string Ime { get; set; }
        
        public string Prezime { get; set; }
        
        public decimal Potroseno { get; set; }
    }
}