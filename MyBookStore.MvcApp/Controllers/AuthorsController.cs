#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBookStore.MvcApp.Models;
using MyBookStore.MvcApp.Models.EF;

namespace MyBookStore.MvcApp.Controllers;

/// <summary>
/// CRUD операции над таблицей "Авторы".
/// </summary>
public class AuthorsController : Controller
{
    private readonly BookStoreContext _context;

    public AuthorsController(BookStoreContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Список авторов.
    /// </summary>
    /// <returns></returns>
    public async Task<IActionResult> Index()
    {
        return View(await _context.Authors.ToListAsync());
    }

    /// <summary>
    /// Форма создания автора.
    /// </summary>
    /// <returns></returns>
    public IActionResult Create()
    {
        return View();
    }

    /// <summary>
    /// Создать автора.
    /// </summary>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Name,Surname,MiddleName")] Author author)
    {
        if (!ModelState.IsValid)
        {
            return View(author);
        }

        _context.Add(author);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    /// <summary>
    /// Форма редактирования автора
    /// </summary>
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var author = await _context.Authors.FindAsync(id);
        if (author == null)
        {
            return NotFound();
        }

        return View(author);
    }

    /// <summary>
    /// Создать автора.
    /// </summary>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Surname,MiddleName")] Author author)
    {
        if (id != author.Id)
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            return View(author);
        }

        try
        {
            _context.Update(author);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!AuthorExists(author.Id))
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

    /// <summary>
    /// Форма удаления автора.
    /// </summary>
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var author = await _context.Authors
            .FirstOrDefaultAsync(m => m.Id == id);
        if (author == null)
        {
            return NotFound();
        }

        return View(author);
    }

    /// <summary>
    /// Удалить автора.
    /// </summary>
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var author = await _context.Authors.FindAsync(id);
        _context.Authors.Remove(author);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool AuthorExists(int id)
    {
        return _context.Authors.Any(e => e.Id == id);
    }
}