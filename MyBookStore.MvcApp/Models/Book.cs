namespace MyBookStore.MvcApp.Models;

/// <summary>
/// Товары.
/// </summary>
public class Book
{
    public int Id { get; set; }

    /// <summary>
    /// Наименование.
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// Картинка.
    /// </summary>
    public string? ImagePath { get; set; }

    /// <summary>
    /// Дата поступления.
    /// </summary>
    public DateTime ReceiptDate { get; set; }

    /// <summary>
    /// Id типа.
    /// </summary>
    public int BookTypeId { get; set; }

    /// <summary>
    /// Типы.
    /// </summary>
    /// <remarks>Книга может быть в наличии на бумажном и на электронном носителе.</remarks>
    public List<BookType> Types { get; set; } = new();

    /// <summary>
    /// Цена.
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Описание.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Производитель.
    /// </summary>
    public int ManufacterId { get; set; }

    /// <summary>
    /// Производитель.
    /// </summary>
    public Manufacturer Manufacturer { get; set; }

    public List<Review> Reviews { get; set; } = new();
}