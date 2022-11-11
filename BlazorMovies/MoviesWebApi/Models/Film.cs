using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoviesWebApi.Models
{
    public class Film
    {
        public int FilmId { get; set; }
        
        public string Naslov { get; set; }

        public DateTime DatumPocetkaPrikazivanja { get; set; }
        
        public decimal Ulozeno { get; set; }

        public Zanr Zanr { get; set; }
        public int ZanrId { get; set; }
    }
}
