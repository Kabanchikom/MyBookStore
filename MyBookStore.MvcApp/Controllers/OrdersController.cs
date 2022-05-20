#nullable disable
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyBookStore.MvcApp.Models;
using MyBookStore.MvcApp.Models.EF;

namespace MyBookStore.MvcApp.Controllers;

[Authorize]
public class OrdersController : Controller
{
    private readonly BookStoreContext _context;

    /// <summary>
    /// Корзина.
    /// </summary>
    private readonly Cart _cart;

    public OrdersController(BookStoreContext context, Cart cart)
    {
        _context = context;
        _cart = cart;
    }

    /// <summary>
    /// Список заказов.
    /// </summary>
    /// <returns></returns>
    public async Task<IActionResult> Index()
    {
        var bookStoreContext =
            _context
                .Orders
                .Include(o => o.DeliveryType)
                .Include(o => o.Status);
        return View(await bookStoreContext.ToListAsync());
    }

    /// <summary>
    /// Форма создания заказа.
    /// </summary>
    public IActionResult Create()
    {
        ViewData["DeliveryTypeId"] = new SelectList(_context.DeliveryTypes, "Id", "Name");
        ViewData["StatusId"] = new SelectList(_context.OrderStatuses, "Id", "Name");
        return View(new Order());
    }

    /// <summary>
    /// Создать заказ.
    /// </summary>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Order order)
    {
        if (_cart.Lines.Count == 0)
        {
            ModelState.AddModelError("", "Корзина пуста");
        }

        ModelState.Remove("CartLines");
        ModelState.Remove("Status");
        ModelState.Remove("DeliveryType");

        if (ModelState.IsValid)
        {
            order.CartLines = _cart.Lines;
            order.Status = _context
                .OrderStatuses
                .First(x => x.Name == "Создан");
            order.CreatedAt = DateTime.Now;

            _context.AttachRange(order.CartLines.Select(x => x.Book));

            if (order.Id == 0)
            {
                _context.Add(order);
            }


            await _context.SaveChangesAsync();
            return RedirectToAction("List", "BookStore");
        }

        ViewData["DeliveryTypeId"] = new SelectList(_context.DeliveryTypes, "Id", "Name", order.DeliveryTypeId);
        return View(order);
    }

    /// <summary>
    /// Форма редактирования заказа.
    /// </summary>
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var order = await _context.Orders.FindAsync(id);
        if (order == null)
        {
            return NotFound();
        }

        ViewData["DeliveryTypeId"] = new SelectList(_context.DeliveryTypes, "Id", "Name", order.DeliveryTypeId);
        ViewData["StatusId"] = new SelectList(_context.OrderStatuses, "Id", "Name", order.StatusId);
        return View(order);
    }

    /// <summary>
    /// Редактировать заказ.
    /// </summary>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id,
        [Bind("Id,Name,Middlename,Surname,StatusId,DeliveryTypeId,Address,CreatedAt")]
        Order order)
    {
        if (id != order.Id)
        {
            return NotFound();
        }

        ModelState.Remove("CartLines");
        ModelState.Remove("Status");
        ModelState.Remove("DeliveryType");

        if (ModelState.IsValid)
        {
            try
            {
                order.Status = _context.OrderStatuses.First(x => x.Id == order.StatusId);
                order.DeliveryType = _context.DeliveryTypes.First(x => x.Id == order.DeliveryTypeId);
                order.CreatedAt = _context.Orders.AsNoTracking().First(x => x.Id == order.Id).CreatedAt;

                _context.Update(order);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(order.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToAction(nameof(Index));
        }

        ViewData["DeliveryTypeId"] = new SelectList(_context.DeliveryTypes, "Id", "Name", order.DeliveryTypeId);
        ViewData["StatusId"] = new SelectList(_context.OrderStatuses, "Id", "Name", order.StatusId);
        return View(order);
    }

    /// <summary>
    /// Форма удаления заказа.
    /// </summary>
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var order = await _context.Orders
            .Include(o => o.DeliveryType)
            .Include(o => o.Status)
            .Include(o => o.CartLines)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (order == null)
        {
            return NotFound();
        }

        return View(order);
    }

    /// <summary>
    /// Удалить заказ.
    /// </summary>
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var order = await _context
            .Orders
            .Include(o => o.DeliveryType)
            .Include(o => o.Status)
            .Include(o => o.CartLines)
            .FirstOrDefaultAsync(o => o.Id == id);
        _context.Orders.Remove(order);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool OrderExists(int id)
    {
        return _context.Orders.Any(e => e.Id == id);
    }
}