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
    [Display(Name = "Название")]
    public string Title { get; set; }

    /// <summary>
    /// Картинка.
    /// </summary>
    [Display(Name = "Обложка")]
    public string? ImagePath { get; set; }

    /// <summary>
    /// Дата поступления.
    /// </summary>
    [Display(Name = "Дата поступления")]
    public DateTime ReceiptDate { get; set; }

    /// <summary>
    /// Типы.
    /// </summary>
    /// <remarks>Книга может быть в наличии на бумажном и на электронном носителе.</remarks>
    [Display(Name = "Тип издания")]
    public List<BookType> Types { get; set; } = new();

    /// <summary>
    /// Цена.
    /// </summary>
    [Display(Name = "Цена")]
    public decimal Price { get; set; }

    /// <summary>
    /// Описание.
    /// </summary>
    [Display(Name = "Описание")]
    public string? Description { get; set; }

    /// <summary>
    /// Id Производителя.
    /// </summary>
    public int ManufacturerId { get; set; }

    /// <summary>
    /// Производитель.
    /// </summary>
    [Display(Name = "Производитель")]
    public Manufacturer? Manufacturer { get; set; }

    /// <summary>
    /// Оценка.
    /// </summary>
    /// <remarks>Сколько звезд.</remarks>
    [Range(0, 5, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
    [Display(Name = "Оценка")]
    public decimal Rating { get; set; }

    /// <summary>
    /// Участвует ли в распродаже.
    /// </summary>
    [Display(Name = "Распродажа")]
    public bool IsOnSale { get; set; }

    /// <summary>
    /// Величина скидки.
    /// </summary>
    /// <remarks>В процентах.</remarks>
    [Range(0, 1, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
    [Display(Name = "Скидка")]
    public decimal Discount { get; set; }

    [Display(Name = "Отзывы")] public List<Review> Reviews { get; set; } = new();

    [Display(Name = "Авторы")] public List<Author> Authors { get; set; } = new();

    [Display(Name = "Жанры")] public List<Genre> Genres { get; set; } = new();
}