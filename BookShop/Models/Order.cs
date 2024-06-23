using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace BookShop.Models;

public class Order
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "Datum je obavezan!")]
    [DisplayName("Datum narudžbine")]
    public DateTime OrderDate { get; set; }


    [Required(ErrorMessage = "Korisnik je obavezan!")]
    public string? UserId { get; set; }

    [ForeignKey(nameof(UserId))]
    [DisplayName("Korisnik")]
    public IdentityUser? User { get; set; }


    public ICollection<OrderItem>? OrderItems { get; set; } = [];
}
