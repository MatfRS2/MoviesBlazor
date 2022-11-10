using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoviesWebApi.Models
{
    public class Film
    {
        [Key]
        [Column("Id")]
        public int FilmId { get; set; }
        
        [StringLength(250)]
        [Required]
        public string Naslov { get; set; }

        [DataType(DataType.Date)]
        public DateTime DatumPocetkaPrikazivanja { get; set; }
        
        [Column(TypeName = "money")] 
        public decimal Ulozeno { get; set; }

        public Zanr Zanr { get; set; }
        public int ZanrId { get; set; }
    }
}
