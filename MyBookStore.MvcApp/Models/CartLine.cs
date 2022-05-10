namespace MyBookStore.MvcApp.Models;

/// <summary>
/// Позиция в корзине.
/// </summary>
public class CartLine
{
    public int Id { get; set; }
    public Book Book { get; set; }
    public int Quantity { get; set; }

    public decimal Total => Book.Price * Quantity;
}