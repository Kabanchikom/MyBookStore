using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MyBookStore.MvcApp.Models;
using MyBookStore.MvcApp.Models.EF;
using MyBookStore.MvcApp.Models.ViewModels;

namespace MyBookStore.MvcApp.Controllers;

public class BookStoreController : Controller
{
    private readonly ILogger<BookStoreController> _logger;
    private readonly BookStoreContext _context;

    public BookStoreController(ILogger<BookStoreController> logger, BookStoreContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<IActionResult> List(
        int pageNumber = 1,
        int pageSize = 12,
        int? manufacturerId = null,
        SortOrder sortOrder = SortOrder.Ascending,
        SortBy sortBy = SortBy.Price)
    {
        IQueryable<Book> books = _context
            .Books
            .Include(x => x.Manufacturer)
            .Include(x => x.Types);

        var filteredBooks = books;

        if (manufacturerId != null)
        {
            filteredBooks = books
                .Where(x => x.ManufacturerId == manufacturerId);
        }

        var sortedBooks = sortOrder == SortOrder.Ascending
            ? filteredBooks.OrderBy(b => b.Price)
            : filteredBooks.OrderByDescending(x => x.Price);

        var pagedBooks = await sortedBooks
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var viewModel = new BookStoreListViewModel(
            pageNumber,
            pageSize,
            await sortedBooks.CountAsync(),
            manufacturerId,
            sortOrder,
            sortBy,
            pagedBooks,
            await _context.Manufacturers.ToListAsync());

        return View(viewModel);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public async Task<IActionResult> Book(int bookId)
    {
        var book = _context
            .Books
            .Include(x => x.Manufacturer)
            .Include(x => x.Types)
            .Include(x => x.Authors)
            .Include(x => x.Genres)
            .Include(x => x.Reviews)
            .FirstOrDefault(x => x.Id == bookId);

        return View(book);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
    }
}