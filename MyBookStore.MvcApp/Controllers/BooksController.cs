#nullable disable
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyBookStore.MvcApp.Models;
using MyBookStore.MvcApp.Models.DTO;
using MyBookStore.MvcApp.Models.EF;

namespace MyBookStore.MvcApp.Controllers;

/// <summary>
/// CRUD операции над таблицей "Книги".
/// </summary>
[Authorize]
public class BooksController : Controller
{
    private readonly BookStoreContext _context;
    private readonly IMapper _mapper;

    public BooksController(BookStoreContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    /// <summary>
    /// Таблица.
    /// </summary>
    /// <returns></returns>
    public async Task<IActionResult> Index()
    {
        var bookStoreContext = _context
            .Books
            .Include(b => b.Manufacturer)
            .Include(x => x.Authors)
            .Include(x => x.Genres);
        return View(await bookStoreContext.ToListAsync());
    }

    /// <summary>
    /// Подробная информация о книге.
    /// </summary>
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var book = await _context.Books
            .Include(b => b.Manufacturer)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (book == null)
        {
            return NotFound();
        }

        return View(book);
    }

    /// <summary>
    /// Форма создания книги.
    /// </summary>
    public IActionResult Create()
    {
        ViewData["ManufacturerId"] = new SelectList(_context.Manufacturers, "Id", "Name");
        ViewData["Authors"] = new MultiSelectList(_context.Authors, "Id", "");
        ViewData["Genres"] = new MultiSelectList(_context.Genres, "Id", "Name");
        ViewData["Types"] = new MultiSelectList(_context.BookTypes, "Id", "Name");
        return View();
    }

    /// <summary>
    /// Создать книгу.
    /// </summary>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        [Bind("Id," +
              "Title," +
              "AuthorIds," +
              "GenreIds," +
              "TypeIds," +
              "ImagePath," +
              "ReceiptDate," +
              "BookTypeId,Price," +
              "Description," +
              "ManufacturerId," +
              "IsOnSale," +
              "Discount")]
        BookDTO bookDto)
    {
        if (ModelState.IsValid)
        {
            var authors = new List<Author>();
            var types = new List<BookType>();
            var genres = new List<Genre>();

            foreach (var authorId in bookDto.AuthorIds)
            {
                authors.Add(await _context
                    .Authors
                    .FirstOrDefaultAsync(x => x.Id == authorId));
            }

            foreach (var typeId in bookDto.TypeIds)
            {
                types.Add(await _context
                    .BookTypes
                    .FirstOrDefaultAsync(x => x.Id == typeId));
            }

            foreach (var genreId in bookDto.GenreIds)
            {
                genres.Add(await _context
                    .Genres
                    .FirstOrDefaultAsync(x => x.Id == genreId));
            }

            var book = _mapper.Map<Book>(bookDto);
            ;
            book.Types = types;
            book.Authors = authors;
            book.Genres = genres;

            _context.Add((object) book);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        ViewData["ManufacturerId"] = new SelectList(_context.Manufacturers, "Id", "Name");
        ViewData["Authors"] = new MultiSelectList(_context.Authors, "Id", "");
        ViewData["Genres"] = new MultiSelectList(_context.Genres, "Id", "Name");
        ViewData["Types"] = new MultiSelectList(_context.BookTypes, "Id", "Name");

        return View(bookDto);
    }

    /// <summary>
    /// Форма редактирования книги.
    /// </summary>
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var book = await _context
            .Books
            .Include(x => x.Authors)
            .Include(x => x.Types)
            .Include(x => x.Genres)
            .FirstOrDefaultAsync(x => x.Id == id);

        var bookDto = _mapper.Map<BookDTO>(book);

        bookDto.TypeIds = book.Types.Select(x => x.Id).ToList();
        bookDto.AuthorIds = book.Authors.Select(x => x.Id).ToList();
        bookDto.GenreIds = book.Genres.Select(x => x.Id).ToList();

        if (bookDto == null)
        {
            return NotFound();
        }

        ViewData["ManufacturerId"] = new SelectList(_context.Manufacturers, "Id", "Name", bookDto.ManufacturerId);
        ViewData["Authors"] = new MultiSelectList(_context.Authors, "Id", "", bookDto.AuthorIds);
        ViewData["Genres"] = new MultiSelectList(_context.Genres, "Id", "Name", bookDto.GenreIds);
        ViewData["Types"] = new MultiSelectList(_context.BookTypes, "Id", "Name", bookDto.TypeIds);

        return View(bookDto);
    }

    /// <summary>
    /// Редактировать книгу.
    /// </summary>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(
        int id,
        [Bind("Id," +
              "Title," +
              "AuthorIds," +
              "GenreIds," +
              "TypeIds," +
              "ImagePath," +
              "ReceiptDate," +
              "BookTypeId,Price," +
              "Description," +
              "ManufacturerId," +
              "IsOnSale," +
              "Discount")]
        BookDTO bookDto)
    {
        if (id != bookDto.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                var authors = new List<Author>();
                var types = new List<BookType>();
                var genres = new List<Genre>();

                foreach (var authorId in bookDto.AuthorIds)
                {
                    authors.Add(await _context
                        .Authors
                        .FirstOrDefaultAsync(x => x.Id == authorId));
                }

                foreach (var typeId in bookDto.TypeIds)
                {
                    types.Add(await _context
                        .BookTypes
                        .FirstOrDefaultAsync(x => x.Id == typeId));
                }

                foreach (var genreId in bookDto.GenreIds)
                {
                    genres.Add(await _context
                        .Genres
                        .FirstOrDefaultAsync(x => x.Id == genreId));
                }

                var book = await _context
                    .Books
                    .Include(x => x.Authors)
                    .Include(x => x.Types)
                    .Include(x => x.Genres)
                    .FirstOrDefaultAsync(x => x.Id == bookDto.Id);

                _mapper.Map(bookDto, book);
                book.Types = types;
                book.Authors = authors;
                book.Genres = genres;

                _context.Update(book);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(bookDto.Id))
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

        ViewData["ManufacturerId"] = new SelectList(_context.Manufacturers, "Id", "Name", bookDto.ManufacturerId);
        ViewData["Authors"] = new MultiSelectList(_context.Authors, "Id", "", bookDto.AuthorIds);
        ViewData["Genres"] = new MultiSelectList(_context.Genres, "Id", "Name", bookDto.GenreIds);
        ViewData["Types"] = new MultiSelectList(_context.BookTypes, "Id", "Name", bookDto.TypeIds);

        return View(bookDto);
    }

    /// <summary>
    /// Форма удаления книги.
    /// </summary>
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var book = await _context.Books
            .Include(b => b.Manufacturer)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (book == null)
        {
            return NotFound();
        }

        return View(book);
    }

    /// <summary>
    /// Удалить книгу.
    /// </summary>
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var book = await _context.Books.FindAsync(id);
        _context.Books.Remove(book);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool BookExists(int id)
    {
        return _context.Books.Any(e => e.Id == id);
    }
}