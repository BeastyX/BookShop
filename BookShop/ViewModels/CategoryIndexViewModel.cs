using BookShop.Models;

namespace BookShop.ViewModels;

public class CategoryIndexViewModel
{
    public const string SORT_ID_ASC = "id_asc";
    public const string SORT_ID_DESC = "id_desc";
    public const string SORT_NAME_ASC = "name_asc";
    public const string SORT_NAME_DESC = "name_desc";

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

    public string CurrentSearch { get; set; } = string.Empty;
    public string CurrentSort { get; set; } = string.Empty;
    public string IdSortParameter { get; set; } = string.Empty;
    public string NameSortParameter { get; set; } = string.Empty;
}
