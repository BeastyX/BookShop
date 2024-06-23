using BookShop.Models;
using System.ComponentModel;

namespace BookShop.ViewModels;

public class ShoppingCartViewModel
{
    public required List<ShoppingCartItem> ShoppingCartItems { get; set; } = [];

    [DisplayName("Ukupna cena")]
    public required double TotalPrice { get; set; }
}
