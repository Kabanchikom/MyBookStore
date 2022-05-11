using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyBookStore.MvcApp.Models;
using MyBookStore.MvcApp.Models.ViewModels;

namespace MyBookStore.MvcApp.Controllers;

public class AccountController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;

    public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    /// <summary>
    /// Форма регистрации.
    /// </summary>
    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    /// <summary>
    /// Зарегистрировать пользователя.
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var user = new User
        {
            Email = model.Email,
            UserName = model.Email,
            Surname = model.Surname,
            Name = model.Name,
            Middlename = model.Middlename,
            PhoneNumber = model.PhoneNumber,
            DateOfBirth = model.DateOfBirth,
        };

        var result = await _userManager.CreateAsync(user, model.Password);

        if (result.Succeeded)
        {
            await _signInManager.SignInAsync(user, false);
            return RedirectToAction("List", "BookStore");
        }
        else
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        return View(model);
    }
}