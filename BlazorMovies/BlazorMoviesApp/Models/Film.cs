using System.ComponentModel.DataAnnotations;

namespace BlazorMoviesApp.Models
{
    public class Film
    {
        public int FilmId { get; set; }
        public string? Naslov { get; set; }
        public DateTime DatumPocetkaPrikazivanja { get; set; }
        public int ZanrId { get; set; }
        public decimal Ulozeno { get; set; }
    }
}
