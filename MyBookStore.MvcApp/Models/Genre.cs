using System.ComponentModel.DataAnnotations;

namespace MyBookStore.MvcApp.Models;

/// <summary>
/// Жанр.
/// </summary>
public class Genre
{
    public int Id { get; set; }

    [Display(Name = "Название")] public string Name { get; set; }

    public List<Book> Books { get; set; } = new();
}