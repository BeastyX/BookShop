using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace BookShop.Models;

public class ShoppingCartItem
{
    [Key]
    public int Id { get; set; }


    [Required(ErrorMessage = "Knjiga je obavezna!")]
    public int BookId { get; set; }

    [ForeignKey(nameof(BookId))]
    [DisplayName("Knjiga")]
    public Book? Book { get; set; }


    [Required(ErrorMessage = "Korisnik je obavezan!")]
    public string? UserId { get; set; }

    [ForeignKey(nameof(UserId))]
    [DisplayName("Korisnik")]
    public IdentityUser? User { get; set; }


    [Required(ErrorMessage = "Cena je obavezna!")]
    [DisplayName("Cena")]
    public double Price { get; set; }
}
