using BookShop.Data;
using BookShop.Models;
using BookShop.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BookShop.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = Utilities.USER_ADMIN_ROLE)]
public class BooksController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly IWebHostEnvironment _hostEnvironment;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0290:Use primary constructor", Justification = "<Pending>")]
    public BooksController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
    {
        _context = context;
        _hostEnvironment = hostEnvironment;
    }


    // GET: Books
    public IActionResult Index()
    {
        var items = _context.Books.Include(o => o.Category);
        return View(items);
    }

    // GET: Books/Details/5
    public IActionResult Details(int? id)
    {
        if (id == null)
            return NotFound();

        var itemFromDB = _context.Books.Include(o => o.Category).FirstOrDefault(o => o.Id == id);
        if (itemFromDB == null)
            return NotFound();

        return View(itemFromDB);
    }

    // GET: Books/Create
    public IActionResult Create()
    {
        var bookViewModel = new BookViewModel
        {
            Book = new(),
            Categories = GetCategoriesSelectListItems()
        };

        return View(bookViewModel);
    }

    // POST: Books/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(BookViewModel viewModel)
    {
        //- Skini spaces na pocetku i na kraju
        viewModel.Book.Title = viewModel.Book?.Title?.Trim();

        //- Server side validation
        var itemExists = _context.Books.AsEnumerable().Any(o => string.Equals(o.Title, viewModel.Book?.Title, StringComparison.OrdinalIgnoreCase));
        if (itemExists == true)
            ModelState.AddModelError("Book.Title", "Knjiga sa istim imenom već postoji!");

        if (viewModel.CoverImage == null || viewModel.CoverImage.Length <= 0)
            ModelState.AddModelError("CoverImage", "Slika je obavezna!");

        if (ModelState.IsValid)
        {
            SaveCoverImageToUploads(viewModel);

            _context.Add(viewModel.Book!);
            _context.SaveChanges();

            TempData["success"] = "Knjiga uspešno kreirana!";
            return RedirectToAction(nameof(Index));
        }

        //- Repopuplate Categories
        viewModel.Categories = GetCategoriesSelectListItems();
        return View(viewModel);
    }

    // GET: Books/Edit/5
    public IActionResult Edit(int? id)
    {
        if (id is null or 0)
            return NotFound();

        var itemFromDB = _context.Books.FirstOrDefault(o => o.Id == id);
        if (itemFromDB == null)
            return NotFound();

        var bookViewModel = new BookViewModel
        {
            Book = itemFromDB,
            Categories = GetCategoriesSelectListItems()
        };

        return View(bookViewModel);
    }

    // POST: Books/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, BookViewModel viewModel)
    {
        if (id <= Utilities.SEED_NUMBER_BOOKS)
        {
            TempData["error"] = "Nije dozvoljena izmena predefinisanih knjiga!";
            return RedirectToAction(nameof(Index));
        }

        if (id != viewModel.Book.Id)
            return NotFound();

        //- Skini spaces na pocetku i na kraju
        viewModel.Book.Title = viewModel.Book.Title?.Trim();

        if (ModelState.IsValid)
        {
            if (viewModel.CoverImage != null && viewModel.CoverImage.Length > 0)
            {
                DeleteOldImageIfExists(viewModel.Book);
                SaveCoverImageToUploads(viewModel);
            }

            _context.Update(viewModel.Book);
            _context.SaveChanges();

            TempData["success"] = "Knjiga uspešno izmenjena!";
            return RedirectToAction(nameof(Index));
        }

        viewModel.Categories = GetCategoriesSelectListItems();
        return View(viewModel);
    }

    // GET: Books/Delete/5
    public IActionResult Delete(int? id)
    {
        if (id is null or 0)
            return NotFound();

        var itemFromDB = _context.Books.FirstOrDefault(o => o.Id == id);
        if (itemFromDB == null)
            return NotFound();

        return View(itemFromDB);
    }

    // POST: Books/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        if (id <= Utilities.SEED_NUMBER_BOOKS)
        {
            TempData["error"] = "Nije dozvoljeno brisanje predefinisanih knjiga!";
            return RedirectToAction(nameof(Index));
        }

        var itemFromDB = _context.Books.FirstOrDefault(o => o.Id == id);
        if (itemFromDB == null)
            return NotFound();

        // todo: proveri da li je povezana sa externim bazama pre nego sto obrises

        DeleteOldImageIfExists(itemFromDB);

        _context.Books.Remove(itemFromDB);
        _context.SaveChanges();

        TempData["success"] = "Knjiga uspešno obrisana!";
        return RedirectToAction(nameof(Index));
    }

    private IQueryable<SelectListItem> GetCategoriesSelectListItems() => _context.Categories.Select(o => new SelectListItem(o.Name, o.Id.ToString()));

    private void SaveCoverImageToUploads(BookViewModel viewModel)
    {
        if (viewModel.CoverImage != null && viewModel.CoverImage.Length > 0)
        {
            var uploads = Path.Combine(_hostEnvironment.WebRootPath, Utilities.UPLOAD_IMAGE_PATH);
            var filename = $"{viewModel.Book!.Title}-{viewModel.Book!.Author}-{viewModel.CoverImage.FileName}";
            var filePath = Path.Combine(uploads, filename);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                viewModel.CoverImage.CopyTo(stream);
            }

            viewModel.Book.CoverImagePath = $"{Path.DirectorySeparatorChar}{Utilities.UPLOAD_IMAGE_PATH}{Path.DirectorySeparatorChar}{filename}";
        }
    }

    private void DeleteOldImageIfExists(Book book)
    {
        if (!string.IsNullOrEmpty(book.CoverImagePath))
        {
            var oldImagePath = Path.Combine(_hostEnvironment.WebRootPath, book.CoverImagePath.TrimStart(Path.DirectorySeparatorChar));

            if (System.IO.File.Exists(oldImagePath))
                System.IO.File.Delete(oldImagePath);
        }
    }
}
