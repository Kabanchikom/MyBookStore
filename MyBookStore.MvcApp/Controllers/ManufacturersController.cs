#nullable disable
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBookStore.MvcApp.Models;
using MyBookStore.MvcApp.Models.EF;

namespace MyBookStore.MvcApp.Controllers;

/// <summary>
/// CRUD операции над таблицей "Издатели".
/// </summary>
[Authorize]
public class ManufacturersController : Controller
{
    private readonly BookStoreContext _context;

    public ManufacturersController(BookStoreContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Список издателей.
    /// </summary>
    public async Task<IActionResult> Index()
    {
        return View(await _context.Manufacturers.ToListAsync());
    }

    /// <summary>
    /// Форма создания издателя.
    /// </summary>
    public IActionResult Create()
    {
        return View();
    }

    /// <summary>
    /// Создать издателя.
    /// </summary>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Name")] Manufacturer manufacturer)
    {
        if (ModelState.IsValid)
        {
            _context.Add(manufacturer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        return View(manufacturer);
    }

    /// <summary>
    /// Форма редактирования издателя.
    /// </summary>
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var manufacturer = await _context.Manufacturers.FindAsync(id);
        if (manufacturer == null)
        {
            return NotFound();
        }

        return View(manufacturer);
    }

    /// <summary>
    /// Редактировать издателя.
    /// </summary>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Manufacturer manufacturer)
    {
        if (id != manufacturer.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(manufacturer);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ManufacturerExists(manufacturer.Id))
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

        return View(manufacturer);
    }

    /// <summary>
    /// Форма удаления издателя.
    /// </summary>
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var manufacturer = await _context.Manufacturers
            .FirstOrDefaultAsync(m => m.Id == id);
        if (manufacturer == null)
        {
            return NotFound();
        }

        return View(manufacturer);
    }

    /// <summary>
    /// Удалить издателя.
    /// </summary>
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var manufacturer = await _context.Manufacturers.FindAsync(id);
        _context.Manufacturers.Remove(manufacturer);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool ManufacturerExists(int id)
    {
        return _context.Manufacturers.Any(e => e.Id == id);
    }
}