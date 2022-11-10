using System.ComponentModel.DataAnnotations;

namespace MoviesMauiApp.Models
{
    public class Film
    {
        public int Id { get; set; }
        public string? Naslov { get; set; }
        public DateTime DatumPocetkaPrikazivanja { get; set; }
        public string? Zanr { get; set; }
        public decimal Ulozeno { get; set; }
    }
}
