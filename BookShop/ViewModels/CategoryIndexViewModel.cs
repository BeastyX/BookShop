using BookShop.Models;

namespace BookShop.ViewModels;

public class CategoryIndexViewModel
{
    public List<Category> Categories { get; set; } = [];
    public int PageNumber { get; set; }
    public int TotalPages { get; set; }
    public bool HasPreviousPage
    {
        get => PageNumber > 1;
    }

    public bool HasNextPage
    {
        get => PageNumber < TotalPages;
    }
}
