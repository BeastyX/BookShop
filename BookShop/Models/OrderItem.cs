using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace BookShop.Models;

public class OrderItem
{
    [Key]
    public int Id { get; set; }


    [Required(ErrorMessage = "Narudžbina je obavezna!")]
    public int OrderId { get; set; }

    [ForeignKey(nameof(OrderId))]
    [DisplayName("Narudžbina")]
    public Order? Order { get; set; }


    [Required(ErrorMessage = "Knjiga je obavezna!")]
    public int BookId { get; set; }

    [ForeignKey(nameof(BookId))]
    [DisplayName("Knjiga")]
    public Book? Book { get; set; }


    [Required(ErrorMessage = "Cena je obavezna!")]
    [DisplayName("Cena")]
    public double Price { get; set; }
}
