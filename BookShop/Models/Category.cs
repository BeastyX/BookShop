using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BookShop.Models
{
    public class Category
    {
        [Key] //- Moze atribut [Key] ali i ne mora ako je Id ili CategoryId, onda se podrazumeva
        public int Id { get; set; }

        [Required(ErrorMessage = "Naziv kategorije je obavezan!")]
        [MaxLength(100)]
        [DisplayName("Naziv kategorije")]
        public string Name { get; set; } = string.Empty;
    }
}
