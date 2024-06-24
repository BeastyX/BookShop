using BookShop.Data;
using BookShop.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.Areas.Admin.Controllers.Api;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController : Controller
{
    private readonly ApplicationDbContext _context;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0290:Use primary constructor", Justification = "<Pending>")]
    public CategoriesController(ApplicationDbContext context) => _context = context;


    // GET: api/Categories
    [HttpGet]
    public IActionResult GetCategories()
    {
        var categories = _context.Categories.ToList();
        return Ok(categories);
    }

    // GET: api/Categories/{id}
    [HttpGet("{id}")]
    public IActionResult GetCategory(int id)
    {
        var category = _context.Categories.FirstOrDefault(c => c.Id == id);
        if (category == null)
            return NotFound();

        return Ok(category);
    }

    // POST: api/Categories
    [HttpPost]
    public IActionResult CreateCategory([FromBody] Category category)
    {
        if (category == null)
            return BadRequest("Nema kategorije!");

        //- Skini spaces na pocetku i na kraju
        category.Name = category.Name?.Trim();

        var categoryExists = _context.Categories.ToList().Any(o => string.Equals(o.Name, category.Name, StringComparison.OrdinalIgnoreCase));
        if (categoryExists)
            return Conflict("Kategorija sa istim imenom već postoji!");

        _context.Add(category);
        _context.SaveChanges();

        return Ok(category);
    }

    // PUT: api/Categories/{id}
    [HttpPut("{id}")]
    public IActionResult EditCategory(int id, [FromBody] Category category)
    {
        if (category == null || id != category.Id)
            return BadRequest();

        if (id <= Utilities.SEED_NUMBER_CATEGORIES)
            return Conflict("Nije dozvoljena izmena predefinisanih kategorija!");

        category.Name = category.Name.Trim();

        var categoryExists = _context.Categories.ToList().Any(o => string.Equals(o.Name, category.Name, StringComparison.OrdinalIgnoreCase));
        if (categoryExists)
            return Conflict("Kategorija sa istim imenom već postoji!");

        var categoryInDb = _context.Categories.FirstOrDefault(c => c.Id == id);
        if (categoryInDb == null)
            return BadRequest();

        categoryInDb.Name = category.Name;
        _context.Update(categoryInDb);
        _context.SaveChanges();

        return Ok(categoryInDb);
    }

    // DELETE: api/Categories/{id}
    [HttpDelete("{id}")]
    public IActionResult DeleteCategory(int id)
    {
        if (id <= Utilities.SEED_NUMBER_CATEGORIES)
            return Conflict("Nije dozvoljeno brisanje predefinisanih kategorija!");

        var category = _context.Categories.FirstOrDefault(c => c.Id == id);
        if (category == null)
            return BadRequest();

        // Provera da li postoje knjige koje koriste ovu kategoriju
        bool hasRelatedBooks = _context.Books.Any(b => b.CategoryId == id);
        if (hasRelatedBooks)
            return Conflict("Nije dozvoljeno brisanje kategorije koja je već povezana sa knjigama!");

        _context.Categories.Remove(category);
        _context.SaveChanges();

        return Ok(category);
    }
}
