namespace MyBookStore.MvcApp.Models;

/// <summary>
/// Заказы.
/// </summary>
public class Order
{
    public int Id { get; set; }

    // /// <summary>
    // /// Id клиента.
    // /// </summary>
    // public int UserId { get; set; }

    // /// <summary>
    // /// Клиент.
    // /// </summary>
    // public User User { get; set; }

    /// <summary>
    /// Id статуса заказа.
    /// </summary>
    public int StatusId { get; set; }

    /// <summary>
    /// Статус заказа.
    /// </summary>
    public OrderStatus OrderStatus { get; set; }

    /// <summary>
    /// Дата заказа.
    /// </summary>
    public DateTime CreatedAt { get; set; }
}