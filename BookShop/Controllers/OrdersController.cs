using BookShop.Data;
using BookShop.Models;
using BookShop.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookShop.Controllers;

[Authorize(Roles = Utilities.USER_CUSTOMER_ROLE)]
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
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
            return NotFound();

        var items = await _context.Orders
            .Where(o => o.UserId == user.Id)
            .Include(o => o.OrderItems)
            .ToListAsync();


        return View(items);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateOrder()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
            return NotFound();

        var shoppingCartItems = await _context.ShoppingCartItems
            .Where(o => o.UserId == user.Id)
            .Include(o => o.Book)
            .ToListAsync();

        if (shoppingCartItems.Count == 0)
            return RedirectToAction("Index", "ShoppingCart");

        var order = new Order
        {
            OrderDate = DateTime.Now,
            UserId = user.Id
        };

        foreach (var item in shoppingCartItems)
        {
            var orderItem = new OrderItem
            {
                BookId = item.BookId,
                Price = item.Price
            };

            order.OrderItems?.Add(orderItem);
        }

        _context.Orders.Add(order);
        _context.ShoppingCartItems.RemoveRange(shoppingCartItems);
        await _context.SaveChangesAsync();

        TempData["success"] = "Narudžbina uspešno napravljena!";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Update(int? id)
    {
        if (id is null or 0)
            return NotFound();

        var user = await _userManager.GetUserAsync(User);
        if (user == null)
            return NotFound();

        var item = await _context.Orders
            .Where(o => o.UserId == user.Id)
            .Include(o => o.OrderItems!)
            .ThenInclude(oi => oi.Book)
            .FirstOrDefaultAsync(o => o.Id == id);

        if (item == null)
            return NotFound();

        var viewModel = new OrderViewModel
        {
            Order = item
        };

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(OrderViewModel viewModel, int markedForDeleteId)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
            return NotFound();

        var order = await _context.Orders
            .Include(o => o.OrderItems!)
            .ThenInclude(oi => oi.Book)
            .FirstOrDefaultAsync(o => o.Id == viewModel.Order.Id && o.UserId == user.Id);

        if (order == null)
            return NotFound();

        var deletionList = viewModel.OrderItemsMarkedForDeletion;

        if (!deletionList.Remove(markedForDeleteId))
            deletionList.Add(markedForDeleteId);

        viewModel.OrderItemsMarkedForDeletion = deletionList;
        viewModel.Order = order;

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateConfirmed(OrderViewModel viewModel)
    {
        if (viewModel == null || viewModel.Order == null)
            return NotFound();

        var user = await _userManager.GetUserAsync(User);
        if (user == null)
            return NotFound();

        var order = await _context.Orders
            .Include(o => o.OrderItems!)
            .FirstOrDefaultAsync(o => o.Id == viewModel.Order.Id && o.UserId == user.Id);

        if (order == null)
            return NotFound();

        bool anyItemRemovedFromOrder = false;
        foreach (var orderItemId in viewModel.OrderItemsMarkedForDeletion)
        {
            var orderItem = order.OrderItems?.FirstOrDefault(o => o.Id == orderItemId);
            if (orderItem != null)
            {
                order.OrderItems?.Remove(orderItem);
                _context.OrderItems.Remove(orderItem);
                anyItemRemovedFromOrder = true;
            }
        }

        if (anyItemRemovedFromOrder)
            TempData["success"] = "Narudžbina uspešno izmenjena!";


        if (order.OrderItems != null && order.OrderItems.Count == 0)
        {
            _context.Orders.Remove(order);
            TempData["success"] = "Narudžbina je obrisana jer nema više knjiga!";
        }

        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
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
            TempData["success"] = "Narudžbina uspešno izbrisana!";
        }

        return RedirectToAction(nameof(Index));
    }
}
