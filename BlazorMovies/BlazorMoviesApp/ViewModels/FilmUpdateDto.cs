
using System.ComponentModel.DataAnnotations;

namespace BlazorMoviesApp.ViewModels
{
    public class FilmUpdateDto
    {
        public int FilmId { get; set; }
        public string? Naslov { get; set; }
        public DateTime DatumPocetkaPrikazivanja { get; set; }
        public int ZanrId { get; set; }
        public decimal Ulozeno { get; set; }
    }
}
