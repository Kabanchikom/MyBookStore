using System.ComponentModel.DataAnnotations;

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
    /// Id Производителя.
    /// </summary>
    public int ManufacturerId { get; set; }

    /// <summary>
    /// Производитель.
    /// </summary>
    public Manufacturer Manufacturer { get; set; }

    /// <remarks>Сколько звезд.</remarks>
    [Range(0, 5, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
    public decimal Rating { get; set; }

    /// <summary>
    /// Участвует ли в распродаже.
    /// </summary>
    public bool IsOnSale { get; set; }

    /// <summary>
    /// Величина скидки.
    /// </summary>
    [Range(0, 1, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
    public decimal Discount { get; set; }

    public List<Review> Reviews { get; set; } = new();

    public List<Author> Authors { get; set; } = new();

    public List<Genre> Genres { get; set; } = new();
}