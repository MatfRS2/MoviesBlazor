
using System.ComponentModel.DataAnnotations;

namespace MoviesMauiApp.ViewModels
{
    public class FilmUpdateDTO
    {
        public int Id { get; set; }
        public string? Naslov { get; set; }
        public DateTime DatumPocetkaPrikazivanja { get; set; }
        public string? Zanr { get; set; }
        public decimal Ulozeno { get; set; }
    }
}
