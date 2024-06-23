using BookShop.Data;
using BookShop.Models;
using BookShop.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookShop.Controllers;

[Authorize(Roles = Utilities.USER_CUSTOMER_ROLE)]
public class ShoppingCartController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0290:Use primary constructor", Justification = "<Pending>")]
    public ShoppingCartController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task<IActionResult> Index()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
            return NotFound();

        var items = await _context.ShoppingCartItems
            .Where(o => o.UserId == user.Id)
            .Include(o => o.Book)
            .ToListAsync();

        var viewModel = new ShoppingCartViewModel
        {
            ShoppingCartItems = items,
            TotalPrice = items.Sum(o => o.Price)
        };

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddToCart(int bookId)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
            return NotFound();

        var book = await _context.Books.FindAsync(bookId);
        if (book == null)
            return NotFound();

        var cartItem = new ShoppingCartItem
        {
            BookId = bookId,
            UserId = user.Id,
            Price = book.Price,
        };

        _context.ShoppingCartItems.Add(cartItem);
        await _context.SaveChangesAsync();

        TempData["success"] = "Knjiga je ubačena u korpu!";
        return RedirectToAction(nameof(Index), "Home");

        //return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await _context.ShoppingCartItems.FindAsync(id);
        if (item == null)
            return NotFound();

        _context.ShoppingCartItems.Remove(item);
        await _context.SaveChangesAsync();

        TempData["success"] = "Knjiga je izbačena iz korpe!";
        return RedirectToAction(nameof(Index));
    }

    // Koristim za dinamicko prikazivanje broja knjiga u korpi (badge)
    [HttpGet]
    public async Task<JsonResult> GetCartItemCount()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
            return Json(0);

        var items = await _context.ShoppingCartItems
            .Where(o => o.UserId == user.Id)
            .ToListAsync();

        return Json(items.Count);
    }
}