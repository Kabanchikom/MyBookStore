#nullable disable
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBookStore.MvcApp.Models;
using MyBookStore.MvcApp.Models.EF;

namespace MyBookStore.MvcApp.Controllers;

/// <summary>
/// CRUD операции над таблицей "Типы изданий".
/// </summary>
[Authorize]
public class BookTypesController : Controller
{
    private readonly BookStoreContext _context;

    public BookTypesController(BookStoreContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Список типов изданий.
    /// </summary>
    public async Task<IActionResult> Index()
    {
        return View(await _context.BookTypes.ToListAsync());
    }

    /// <summary>
    /// Форма создания типа издания.
    /// </summary>
    public IActionResult Create()
    {
        return View();
    }

    /// <summary>
    /// Создать тип издания.
    /// </summary>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Name")] BookType bookType)
    {
        if (ModelState.IsValid)
        {
            _context.Add(bookType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        return View(bookType);
    }

    /// <summary>
    /// Форма редактирования типа издания.
    /// </summary>
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var bookType = await _context.BookTypes.FindAsync(id);
        if (bookType == null)
        {
            return NotFound();
        }

        return View(bookType);
    }

    /// <summary>
    /// Редактировать тип издания.
    /// </summary>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] BookType bookType)
    {
        if (id != bookType.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(bookType);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookTypeExists(bookType.Id))
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

        return View(bookType);
    }

    /// <summary>
    /// Форма удаления типа издания.
    /// </summary>
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var bookType = await _context.BookTypes
            .FirstOrDefaultAsync(m => m.Id == id);
        if (bookType == null)
        {
            return NotFound();
        }

        return View(bookType);
    }

    /// <summary>
    /// Удалить тип издания.
    /// </summary>
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var bookType = await _context.BookTypes.FindAsync(id);
        _context.BookTypes.Remove(bookType);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool BookTypeExists(int id)
    {
        return _context.BookTypes.Any(e => e.Id == id);
    }
}