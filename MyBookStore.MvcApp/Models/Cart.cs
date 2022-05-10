namespace MyBookStore.MvcApp.Models;

/// <summary>
/// Корзина.
/// </summary>
public class Cart
{
    public List<CartLine> Lines { get; set; } = new();

    public decimal TotalValue => Lines.Sum(x => x.Book.Price * x.Quantity);

    public virtual void AddItem(Book book, int quantity)
    {
        var line = Lines.FirstOrDefault(x => x.Book.Id == book.Id);

        if (line == null)
        {
            Lines.Add(new CartLine
            {
                Book = book,
                Quantity = quantity
            });
        }
        else
        {
            line.Quantity += quantity;
        }
    }

    public virtual void RemoveLine(Book book) => Lines.RemoveAll(l => l.Book.Id == book.Id);

    public virtual void Clear() => Lines.Clear();
}