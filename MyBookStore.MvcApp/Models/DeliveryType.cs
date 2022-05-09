using System.ComponentModel.DataAnnotations;

namespace MyBookStore.MvcApp.Models;

/// <summary>
/// Услуги.
/// </summary>
/// <remarks>Тип доставки.</remarks>
public class DeliveryType
{
    public int Id { get; set; }

    [Display(Name = "Название")] public string Name { get; set; }
}