#nullable disable
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBookStore.MvcApp.Models;
using MyBookStore.MvcApp.Models.EF;

namespace MyBookStore.MvcApp.Controllers;

/// <summary>
/// CRUD операции над таблицей "Способы доставки".
/// </summary>
[Authorize]
public class DeliveryTypesController : Controller
{
    private readonly BookStoreContext _context;

    public DeliveryTypesController(BookStoreContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Список способов доставки.
    /// </summary>
    public async Task<IActionResult> Index()
    {
        return View(await _context.DeliveryTypes.ToListAsync());
    }

    /// <summary>
    /// Форма создания способа доставки.
    /// </summary>
    public IActionResult Create()
    {
        return View();
    }

    /// <summary>
    /// Создать способ доставки.
    /// </summary>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Name")] DeliveryType deliveryType)
    {
        if (ModelState.IsValid)
        {
            _context.Add(deliveryType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        return View(deliveryType);
    }

    /// <summary>
    /// Форма редактирования способа доставки.
    /// </summary>
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var deliveryType = await _context.DeliveryTypes.FindAsync(id);
        if (deliveryType == null)
        {
            return NotFound();
        }

        return View(deliveryType);
    }

    /// <summary>
    /// Редактировать способ доставки.
    /// </summary>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] DeliveryType deliveryType)
    {
        if (id != deliveryType.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(deliveryType);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeliveryTypeExists(deliveryType.Id))
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

        return View(deliveryType);
    }

    /// <summary>
    /// Форма удаления способа доставки.
    /// </summary>
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var deliveryType = await _context.DeliveryTypes
            .FirstOrDefaultAsync(m => m.Id == id);
        if (deliveryType == null)
        {
            return NotFound();
        }

        return View(deliveryType);
    }

    /// <summary>
    /// Удалить способ доставки.
    /// </summary>
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var deliveryType = await _context.DeliveryTypes.FindAsync(id);
        _context.DeliveryTypes.Remove(deliveryType);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool DeliveryTypeExists(int id)
    {
        return _context.DeliveryTypes.Any(e => e.Id == id);
    }
}