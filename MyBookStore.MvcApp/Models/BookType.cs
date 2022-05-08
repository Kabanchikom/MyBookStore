namespace MyBookStore.MvcApp.Models;

/// <summary>
/// Тип товаров.
/// </summary>
public class BookType
{
    public int Id { get; set; }
    public string Name { get; set; }

    public List<Book> Books { get; set; } = new();
}