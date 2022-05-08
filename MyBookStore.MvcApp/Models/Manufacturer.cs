namespace MyBookStore.MvcApp.Models;

/// <summary>
/// Производитель.
/// </summary>
public class Manufacturer
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Book> Books { get; set; } = new();
}