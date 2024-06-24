using BookShop.Data;
using BookShop.Models;
using BookShop.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = Utilities.USER_ADMIN_ROLE)]
public class CategoriesController : Controller
{
    private ApplicationDbContext _context;
    public CategoriesController(ApplicationDbContext context) => _context = context;

    public IActionResult Index(string sortOrder = CategoryIndexViewModel.SORT_ID_ASC, string searchString = "", int pageNumber = 1)
    {
        var categories = _context.Categories.AsEnumerable();

        if (!string.IsNullOrEmpty(searchString))
            categories = categories.Where(o => o.Name != null && o.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase));

        switch (sortOrder)
        {
            case CategoryIndexViewModel.SORT_ID_ASC:
                categories = categories.OrderBy(o => o.Id);
                break;

            case CategoryIndexViewModel.SORT_ID_DESC:
                categories = categories.OrderByDescending(o => o.Id);
                break;

            case CategoryIndexViewModel.SORT_NAME_ASC:
                categories = categories.OrderBy(o => o.Name);
                break;

            case CategoryIndexViewModel.SORT_NAME_DESC:
                categories = categories.OrderByDescending(o => o.Name);
                break;

            default:
                categories = categories.OrderBy(o => o.Id);
                break;
        }

        //- Server-Side paginacija
        int pageSize = Utilities.NUMBER_CATEGORIES_PER_PAGE; // Broj stavki po stranici
        int totalItems = categories.Count();
        var pagedCategories = categories.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

        var viewModel = new CategoryIndexViewModel
        {
            Categories = pagedCategories,
            PageNumber = pageNumber,
            TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize),
            CurrentSearch = searchString,
            CurrentSort = sortOrder,
            IdSortParameter = sortOrder == CategoryIndexViewModel.SORT_ID_ASC
                ? CategoryIndexViewModel.SORT_ID_DESC 
                : CategoryIndexViewModel.SORT_ID_ASC,

            NameSortParameter = sortOrder == CategoryIndexViewModel.SORT_NAME_ASC 
                ? CategoryIndexViewModel.SORT_NAME_DESC 
                : CategoryIndexViewModel.SORT_NAME_ASC
        };

        return View(viewModel);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Category model)
    {
        //return Content($"{model.Id}, {model.Name}");

        if (model == null)
            return View();

        model.Name = model.Name.Trim();

        if (_context.Categories.ToList().Any(c => string.Equals(model.Name, c.Name, StringComparison.OrdinalIgnoreCase)))
        {
            ModelState.AddModelError("Name", "Postoji kategorija sa istim imenom!");
        }

        if (ModelState.IsValid)
        {
            _context.Add(model);
            _context.SaveChanges();

            TempData["Success"] = "Kategorija uspešno napravljena!";

            return RedirectToAction(nameof(Index));
        }

        return View();
    }

    [HttpGet]
    public IActionResult Update(int id)
    {
        var item = _context.Categories.FirstOrDefault(c => c.Id == id);

        if (item == null)
            return NotFound();

        return View(item);
    }

    [HttpPost]
    [ActionName("Update")]
    [ValidateAntiForgeryToken]
    public IActionResult UpdatePost(int id, Category model)
    {
        if (id <= Utilities.SEED_NUMBER_CATEGORIES)
        {
            TempData["Error"] = "Nije dozvoljena izmena predefinisanih kategorija!";
            return RedirectToAction(nameof(Index));
        }

        if (model == null)
            return NotFound();

        model.Name = model.Name.Trim();

        bool nameExists = _context.Categories
            .Where(c => c.Id != model.Id)
            .ToList()
            .Any(c => string.Equals(model.Name, c.Name, StringComparison.OrdinalIgnoreCase));

        if (nameExists)
        {
            ModelState.AddModelError("Name", "Postoji kategorija sa istim imenom!");
        }

        if (ModelState.IsValid)
        {
            _context.Update(model);
            _context.SaveChanges();

            TempData["Success"] = "Kategorija uspešno izmenjena!";

            return RedirectToAction(nameof(Index));
        }

        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(int id)
    {
        if (id <= Utilities.SEED_NUMBER_CATEGORIES)
        {
            TempData["Error"] = "Nije dozvoljeno brisanje predefinisanih kategorija!";
            return RedirectToAction(nameof(Index));
        }

        var item = _context.Categories.FirstOrDefault(c => c.Id == id);
        if (item == null)
            return NotFound();

        var hasRelatedBooks = _context.Books.Any(b => b.CategoryId == id);
        if (hasRelatedBooks)
        {
            TempData["Error"] = "Nije dozvoljeno brisanje kategorije koja je već povezana sa knjigama!";
            return RedirectToAction(nameof(Index));
        }

        _context.Categories.Remove(item);
        _context.SaveChanges();

        TempData["Success"] = "Kategorija uspešno obrisana!";
        return RedirectToAction(nameof(Index));
    }

    public IActionResult ApiTest()
    {
        var categories = _context.Categories.ToList();

        return View(categories);
    }
}
