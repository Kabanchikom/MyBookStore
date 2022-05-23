using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MyBookStore.MvcApp.Models;

/// <summary>
/// Заказы.
/// </summary>
public class Order
{
    public int Id { get; set; }
    
    /// <summary>
    /// Имя получателя.
    /// </summary>
    [Required]
    [DisplayName("Имя")]
    public string Name { get; set; }

    /// <summary>
    /// Отчество/среднее имя получателя.
    /// </summary>
    [DisplayName("Отчество")]
    public string Middlename { get; set; }

    /// <summary>
    /// Фамилия получателя.
    /// </summary>
    [Required]
    [DisplayName("Фамилия")]
    public string Surname { get; set; }

    /// <summary>
    /// Id статуса заказа.
    /// </summary>
    [DisplayName("Статус заказа")]
    public int StatusId { get; set; }

    /// <summary>
    /// Статус заказа.
    /// </summary>
    [DisplayName("Статус заказа")]
    public OrderStatus Status { get; set; }

    /// <summary>
    /// Id способа доставки.
    /// </summary>
    [Required]
    [DisplayName("Способ доставки")]
    public int DeliveryTypeId { get; set; }

    /// <summary>
    /// Способ доставки.
    /// </summary>
    [DisplayName("Способ доставки")]
    public DeliveryType DeliveryType { get; set; }

    /// <summary>
    /// Адрес доставки.
    /// </summary>
    [Required]
    [DisplayName("Адрес доставки")]
    public string Address { get; set; }

    /// <summary>
    /// Дата заказа.
    /// </summary>
    [DisplayName("Дата заказа")]
    public DateTime CreatedAt { get; set; }
    
    // /// <summary>
    // /// Id клиента.
    // /// </summary>
    // public int CreatedById { get; set; }
    //
    // /// <summary>
    // /// Клиент.
    // /// </summary>
    // public User CreatedBy { get; set; }

    /// <summary>
    /// Позиции в корзине.
    /// </summary>
    [DisplayName("Товары")]
    public List<CartLine> CartLines { get; set; }
}