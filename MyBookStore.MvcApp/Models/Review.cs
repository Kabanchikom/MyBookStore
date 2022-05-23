using System.ComponentModel.DataAnnotations;

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
    [Required(ErrorMessage = "Не указан заголовок")]
    public string Header { get; set; }

    /// <summary>
    /// Текст.
    /// </summary>
    [Required(ErrorMessage = "Не указано тело отзыва")]
    public string Body { get; set; }
    
    /// <summary>
    /// Оценка.
    /// </summary>
    [Required]
    [Range(0, 5, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
    public int Rating { get; set; }

    /// <summary>
    /// Id клиента.
    /// </summary>
    [Required]
    public string CreatedById { get; set; }
    
    /// <summary>
    /// Клиент.
    /// </summary>
    public User CreatedBy { get; set; }
    
    /// <summary>
    /// Дата и время создания.
    /// </summary>
    [Required]
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Id товара.
    /// </summary>
    [Required]
    public int BookId { get; set; }

    /// <summary>
    /// Товар.
    /// </summary>
    public Book Book { get; set; }
}