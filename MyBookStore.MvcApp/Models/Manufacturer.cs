using System.ComponentModel.DataAnnotations;

namespace MyBookStore.MvcApp.Models;

/// <summary>
/// Производитель.
/// </summary>
/// <remarks>Издатель.</remarks>
public class Manufacturer
{
    public int Id { get; set; }

    [Display(Name = "Название")] public string Name { get; set; }
    public List<Book> Books { get; set; } = new();
}