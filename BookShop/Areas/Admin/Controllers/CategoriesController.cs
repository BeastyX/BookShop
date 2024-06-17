using BookShop.Data;
using BookShop.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.Areas.Admin.Controllers;

[Area("Admin")]
public class CategoriesController : Controller
{
    private ApplicationDbContext _context;
    public CategoriesController(ApplicationDbContext context) => _context = context;

    public IActionResult Index()
    {
        var kategorije = _context.Categories.ToList();

        return View(kategorije);
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
        if (id <= 6)
            ModelState.AddModelError("Name", "Nije dozvoljena izmena predefinisanih kategorija!");

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
            return RedirectToAction(nameof(Index));
        }

        return View();
    }
}
