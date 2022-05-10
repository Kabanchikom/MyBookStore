using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBookStore.MvcApp.Infrastructure;
using MyBookStore.MvcApp.Models;
using MyBookStore.MvcApp.Models.EF;
using MyBookStore.MvcApp.Models.ViewModels;

namespace MyBookStore.MvcApp.Controllers;

/// <summary>
/// Операции над корзиной.
/// </summary>
public class CartController : Controller
{
    private readonly BookStoreContext _context;

    public CartController(BookStoreContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Корзина.
    /// </summary>
    /// <param name="returnUrl">Адрес страницы для возврата.</param>
    public IActionResult Index(string returnUrl)
    {
        return View(new CartIndexViewModel
        {
            Cart = GetCart(),
            ReturnUrl = returnUrl
        });
    }

    /// <summary>
    /// Добавить в корзину.
    /// </summary>
    /// <param name="returnUrl">Адрес страницы для возврата.</param>
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

        var cart = GetCart();
        cart.AddItem(book, quantity);
        SaveCart(cart);

        return RedirectToAction("Index", new {returnUrl});
    }

    /// <summary>
    /// Удалить из корзины.
    /// </summary>
    /// <param name="returnUrl">Адрес страницы для возврата.</param>
    public IActionResult RemoveFromCart(int bookId, string returnUrl)
    {
        var book = _context
            .Books
            .FirstOrDefault(p => p.Id == bookId);

        if (book == null)
        {
            return RedirectToAction("Index", new {returnUrl});
        }

        var cart = GetCart();
        cart.RemoveLine(book);
        SaveCart(cart);

        return RedirectToAction("Index", new {returnUrl});
    }

    /// <summary>
    /// Получить объект корзины из состояния сеанса.
    /// </summary>
    private Cart GetCart()
    {
        var cart = HttpContext.Session.GetJson<Cart>("Cart") ?? new Cart();
        return cart;
    }

    /// <summary>
    /// Сохранить объект корзины в состоянии сеанса.
    /// </summary>
    private void SaveCart(Cart cart)
    {
        HttpContext.Session.SetJson("Cart", cart, true);
    }
}