using BookShop.Data;
using BookShop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BookShop.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = Utilities.USER_ADMIN_ROLE)]
public class OrdersController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0290:Use primary constructor", Justification = "<Pending>")]
    public OrdersController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }


    public async Task<IActionResult> Index()
    {
        var items = await _context.Orders
            .Include(o => o.User)
            .Include(o => o.OrderItems)
            .ToListAsync();

        return View(items);
    }

    public IActionResult Create()
    {
        ViewData["UsersList"] = GetSelectListItems_Users;

        ViewData["BookModel"] = _context.Books.FirstOrDefault();
        ViewData["BooksList"] = GetSelectListItems_Books;

        var order = new Order();
        return View(order);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Order order, List<int> books)
    {
        if (books == null || books.Count <= 0)
            ModelState.AddModelError("Books", "Obavezna je bar jedna knjiga!");

        if (order == null || order.UserId == null)
            ModelState.AddModelError("UserId", "Nema korisnika!");

        // New Order
        var newOrder = new Order
        {
            OrderDate = DateTime.Now,
            UserId = order!.UserId,
        };

        // Order Items
        foreach (var bookId in books!)
        {
            var book = await _context.Books.FirstOrDefaultAsync(b => b.Id == bookId);
            if (book == null)
                continue;

            var orderItem = new OrderItem
            {
                BookId = bookId,
                Price = book.Price,
            };

            newOrder.OrderItems!.Add(orderItem);
        }

        if (ModelState.IsValid)
        {
            _context.Orders.Add(newOrder);
            await _context.SaveChangesAsync();

            TempData["success"] = "Narudžbina uspešno napravljena!";
            return RedirectToAction(nameof(Index));
        }

        //- Greska
        ViewData["UsersList"] = GetSelectListItems_Users;

        ViewData["BookModel"] = _context.Books.FirstOrDefault();
        ViewData["BooksList"] = GetSelectListItems_Books;

        return View(order);
    }

    private List<SelectListItem> GetSelectListItems_Users => _context.Users
        .Where(u => u.UserName != Utilities.USER_ADMIN_NAME)
        .Select(u => new SelectListItem(u.UserName, u.Id.ToString()))
        .ToList();

    private List<SelectListItem> GetSelectListItems_Books => _context.Books
        .Select(b => new SelectListItem($"{b.Title} - {b.Author} \xA0\xA0\xA0\xA0 ({Utilities.GetPriceInDinars(b.Price)})", b.Id.ToString()))
        .ToList();

    public async Task<IActionResult> Edit(int? id)
    {
        if (id is null or 0)
            return NotFound();

        var item = await _context.Orders
            .Include(o => o.OrderItems!)
            .ThenInclude(oi => oi.Book)
            .FirstOrDefaultAsync(o => o.Id == id);

        if (item == null)
            return NotFound();

        return View(item);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await _context.Orders.FindAsync(id);
        if (item != null)
        {
            _context.Orders.Remove(item);
            await _context.SaveChangesAsync();
        }

        TempData["success"] = "Narudžbina uspešno izbrisana!";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteOrderItem(int orderId, int orderItemId)
    {
        var order = await _context.Orders
            .Include(o => o.OrderItems)
            .FirstOrDefaultAsync(o => o.Id == orderId);

        if (order == null)
            return NotFound();

        var item = order.OrderItems?
            .FirstOrDefault(oi => oi.Id == orderItemId);

        if (item == null)
            return NotFound();

        _context.OrderItems.Remove(item);
        await _context.SaveChangesAsync();

        if (order.OrderItems != null && order.OrderItems.Count == 0)
        {
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            TempData["success"] = "Narudžbine uspešno izbrisana jer nema stavki!";
            return RedirectToAction(nameof(Index));
        }

        await _context.SaveChangesAsync();

        TempData["success"] = "Stavka narudžbine uspešno izbrisana!";
        return RedirectToAction(nameof(Edit), new { id = orderId });
    }
}