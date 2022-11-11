using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoviesWebApi.Models
{
    public class Zanr
    {
         public int ZanrId { get; set; }

        public string Naziv { get; set; }
        
        public ICollection<Film> Filmovi { get; set; }
    }
}
