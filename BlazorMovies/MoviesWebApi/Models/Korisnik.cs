namespace MoviesWebApi.Models
{
    public class Korisnik
    {
        public int KorisnikId { get; set; }
        
        public string Email { get; set;}
        
        public string Ime { get; set; }
        
        public string Prezime { get; set; }
        
        public decimal Potroseno { get; set; }

        public ICollection<Pretplata> Pretplate { get; set; }
        public ICollection<Paket> Paketi { get; set; }
    }
}