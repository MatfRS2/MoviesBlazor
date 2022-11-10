using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoviesWebApi.Models
{
    public class Zanr
    {
        [Key]
        [Column("Id")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ZanrId { get; set; }

        [StringLength(250)]
        [Required]
        public string Naziv { get; set; }
        
        public ICollection<Film> Films { get; set; }
    }
}
