namespace MoviesWebApi.Models
{
    public class Korisnik
    {
        public string Email { get; set;}
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public decimal Potroseno { get; set; }
        public List<Pretplata> Pretplate { get; set; }
    }
}