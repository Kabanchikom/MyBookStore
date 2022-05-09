using System.ComponentModel.DataAnnotations;

namespace MyBookStore.MvcApp.Models;

/// <summary>
/// Типы товаров.
/// </summary>
/// <remarks>Книга может быть в наличии на бумажном и на электронном носителе.</remarks>
public class BookType
{
    public int Id { get; set; }

    [Display(Name = "Наименование")] public string Name { get; set; }

    public List<Book> Books { get; set; } = new();
}