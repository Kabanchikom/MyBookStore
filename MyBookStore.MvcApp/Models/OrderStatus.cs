namespace MyBookStore.MvcApp.Models;

/// <summary>
/// Статус заказа.
/// </summary>
public class OrderStatus
{
    public int Id { get; set; }
    public string Name { get; set; }

    public List<Order> Orders { get; set; } = new();
}