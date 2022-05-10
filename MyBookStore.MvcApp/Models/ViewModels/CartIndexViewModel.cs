namespace MyBookStore.MvcApp.Models.ViewModels;

public class CartIndexViewModel
{
    public Cart Cart { get; set; }

    /// <summary>
    /// Адрес предыдущей страницы.
    /// </summary>
    public string ReturnUrl { get; set; }
}