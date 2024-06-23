using BookShop.Data;
using BookShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace BookShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0290:Use primary constructor", Justification = "<Pending>")]
        public HomeController(ApplicationDbContext context) => _context = context;

        public IActionResult Index()
        {
            var items = _context.Books.ToList();
            return View(items);
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
                return NotFound();

            var itemFromDB = _context.Books
                .Include(o => o.Category)
                .FirstOrDefault(o => o.Id == id);

            if (itemFromDB == null)
                return NotFound();

            return View(itemFromDB);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
