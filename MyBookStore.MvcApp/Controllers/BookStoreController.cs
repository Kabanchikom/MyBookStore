﻿using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

    public async Task<IActionResult> List(int pageNumber = 1, int pageSize = 12)
    {
        var books = _context
            .Books
            .Include(x => x.Manufacturer)
            .Include(x => x.Types);

        var pagedBooks = await books
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var viewModel = new BookStoreListViewModel(
            pageNumber,
            pageSize,
            await books.CountAsync(),
            pagedBooks);

        return View(viewModel);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult Book()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
    }
}