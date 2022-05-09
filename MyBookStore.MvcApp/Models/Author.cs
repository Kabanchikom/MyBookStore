using System.ComponentModel.DataAnnotations;

namespace MyBookStore.MvcApp.Models;

public class Author
{
    public int Id { get; set; }

    [Display(Name = "Имя")] public string Name { get; set; }

    [Display(Name = "Фамилия")] public string? Surname { get; set; }

    [Display(Name = "Отчество/Среднее имя")]
    public string? MiddleName { get; set; }

    public List<Book> Books { get; set; } = new();

    public override string ToString()
    {
        return $"{Name} {MiddleName} {Surname}";
    }
}