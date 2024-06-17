using BookShop.Data;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.Areas.Admin.Controllers;

[Area("Admin")]
public class CategoriesController : Controller
{
    private ApplicationDbContext _context;
    public CategoriesController(ApplicationDbContext context) => _context = context;

    public IActionResult Index()
    {
        return View();
    }
}
