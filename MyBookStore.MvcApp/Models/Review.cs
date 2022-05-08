namespace MyBookStore.MvcApp.Models;

/// <summary>
/// Отзывы.
/// </summary>
public class Review
{
    public int Id { get; set; }

    /// <summary>
    /// Наименование.
    /// </summary>
    public string Header { get; set; }

    /// <summary>
    /// Текст.
    /// </summary>
    public string Body { get; set; }

    //public int AuthorId { get; set; }

    //public User Author { get; set; }

    /// <summary>
    /// Id товара.
    /// </summary>
    public int BookId { get; set; }

    /// <summary>
    /// Товар.
    /// </summary>
    public Book Book { get; set; }
}