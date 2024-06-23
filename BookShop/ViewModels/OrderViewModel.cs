using BookShop.Models;

namespace BookShop.ViewModels;

public class OrderViewModel
{
    public required Order Order { get; set; }
    public List<int> OrderItemsMarkedForDeletion { get; set; } = [];
}