using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace MyBookStore.MvcApp.Models;

/// <summary>
/// Пользователь.
/// </summary>
public class User : IdentityUser
{
    /// <summary>
    /// Фамилия.
    /// </summary>
    [Required]
    [DisplayName("Фамилия")]
    public string Surname { get; set; }

    /// <summary>
    /// Имя.
    /// </summary>
    [Required]
    [DisplayName("Имя")]
    public string Name { get; set; }

    /// <summary>
    /// Отчество/среднее имя.
    /// </summary>
    [DisplayName("Отчество")]
    public string? Middlename { get; set; }

    /// <summary>
    /// Номер телефона.
    /// </summary>
    [Phone]
    [Required]
    [DisplayName("Номер телефона")]
    public string PhoneNumber { get; set; }

    /// <summary>
    /// Дата рождения.
    /// </summary>
    [Required]
    [DisplayName("Дата рождения")]
    public DateTime DateOfBirth { get; set; }
}