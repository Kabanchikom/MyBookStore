namespace MyBookStore.MvcApp.Models;

public class Author
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Surname { get; set; }
    public string? MiddleName { get; set; }

    public List<Book> Books { get; set; } = new();

    public override string ToString()
    {
        return $"{Name} {MiddleName} {Surname}";
    }
}