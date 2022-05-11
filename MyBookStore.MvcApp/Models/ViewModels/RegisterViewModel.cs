using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MyBookStore.MvcApp.Models.ViewModels;

public class RegisterViewModel
{
    [Required] [Display(Name = "Email")] public string Email { get; set; }

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
    public string Middlename { get; set; }

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

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Пароль")]
    public string Password { get; set; }

    [Required]
    [Compare("Password", ErrorMessage = "Пароли не совпадают")]
    [DataType(DataType.Password)]
    [Display(Name = "Подтвердить пароль")]
    public string PasswordConfirm { get; set; }
}