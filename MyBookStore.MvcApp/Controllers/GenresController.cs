#nullable disable
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBookStore.MvcApp.Models;
using MyBookStore.MvcApp.Models.EF;

namespace MyBookStore.MvcApp.Controllers;

/// <summary>
/// CRUD операции над таблицей "Жанры".
/// </summary>
[Authorize(Roles = "Admin")]
public class GenresController : Controller
{
    private readonly BookStoreContext _context;

    public GenresController(BookStoreContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Список жанров.
    /// </summary>
    public async Task<IActionResult> Index()
    {
        return View(await _context.Genres.ToListAsync());
    }

    /// <summary>
    /// Форма создания жанра.
    /// </summary>
    public IActionResult Create()
    {
        return View();
    }

    /// <summary>
    /// Создать жанр.
    /// </summary>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Name")] Genre genre)
    {
        if (ModelState.IsValid)
        {
            _context.Add(genre);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        return View(genre);
    }

    /// <summary>
    /// Форма редактирования жанра.
    /// </summary>
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var genre = await _context.Genres.FindAsync(id);
        if (genre == null)
        {
            return NotFound();
        }

        return View(genre);
    }

    /// <summary>
    /// Редактировать жанр.
    /// </summary>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Genre genre)
    {
        if (id != genre.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(genre);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GenreExists(genre.Id))
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

        return View(genre);
    }

    /// <summary>
    /// Форма удаления жанра.
    /// </summary>
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var genre = await _context.Genres
            .FirstOrDefaultAsync(m => m.Id == id);
        if (genre == null)
        {
            return NotFound();
        }

        return View(genre);
    }

    /// <summary>
    /// Удалить жанр.
    /// </summary>
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var genre = await _context.Genres.FindAsync(id);
        _context.Genres.Remove(genre);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool GenreExists(int id)
    {
        return _context.Genres.Any(e => e.Id == id);
    }
}