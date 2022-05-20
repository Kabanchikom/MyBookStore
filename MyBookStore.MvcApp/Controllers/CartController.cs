using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBookStore.MvcApp.Models;
using MyBookStore.MvcApp.Models.EF;
using MyBookStore.MvcApp.Models.ViewModels;

namespace MyBookStore.MvcApp.Controllers;

/// <summary>
/// Операции над корзиной.
/// </summary>
[Authorize]
public class CartController : Controller
{
    private readonly BookStoreContext _context;
    private readonly Cart _cart;

    public CartController(BookStoreContext context, Cart cart)
    {
        _context = context;
        _cart = cart;
    }

    /// <summary>
    /// Корзина.
    /// </summary>
    /// <param name="returnUrl">Адрес страницы для возврата.</param>
    [AllowAnonymous]
    public IActionResult Index(string returnUrl)
    {
        return View(new CartIndexViewModel
        {
            Cart = _cart,
            ReturnUrl = returnUrl
        });
    }

    /// <summary>
    /// Добавить в корзину.
    /// </summary>
    /// <param name="returnUrl">Адрес страницы для возврата.</param>
    [AllowAnonymous]
    public IActionResult AddToCart(int bookId, string returnUrl, int quantity = 1)
    {
        var book = _context
            .Books
            .Include(b => b.Manufacturer)
            .Include(x => x.Authors)
            .Include(x => x.Genres)
            .FirstOrDefault(p => p.Id == bookId);

        if (book == null)
        {
            return RedirectToAction("Index", new {returnUrl});
        }

        _cart.AddItem(book, quantity);

        return RedirectToAction("Index", new {returnUrl});
    }

    /// <summary>
    /// Удалить из корзины.
    /// </summary>
    /// <param name="returnUrl">Адрес страницы для возврата.</param>
    [AllowAnonymous]
    public IActionResult RemoveFromCart(int bookId, string returnUrl)
    {
        var book = _context
            .Books
            .FirstOrDefault(p => p.Id == bookId);

        if (book == null)
        {
            return RedirectToAction("Index", new {returnUrl});
        }

        _cart.RemoveLine(book);

        return RedirectToAction("Index", new {returnUrl});
    }
}