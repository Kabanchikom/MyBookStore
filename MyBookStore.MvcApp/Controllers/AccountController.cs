using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBookStore.MvcApp.Models;
using MyBookStore.MvcApp.Models.ViewModels;

namespace MyBookStore.MvcApp.Controllers;

[Authorize]
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
    [AllowAnonymous]
    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    /// <summary>
    /// Зарегистрировать пользователя.
    /// </summary>
    [AllowAnonymous]
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

    [HttpGet]
    [AllowAnonymous]
    public IActionResult Login(string returnUrl = null)
    {
        return View(new LoginViewModel {ReturnUrl = returnUrl});
    }

    [AllowAnonymous]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        ModelState.Remove("ReturnUrl");
        ModelState.Remove("RememberMe");

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var result = await _signInManager
            .PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

        if (result.Succeeded)
        {
            if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
            {
                return Redirect(model.ReturnUrl);
            }
            else
            {
                return RedirectToAction("List", "BookStore");
            }
        }
        else
        {
            ModelState.AddModelError("", "Неправильный логин и (или) пароль");
        }

        return View(model);
    }

    public async Task<IActionResult> Logout()
    {
        // удаляем аутентификационные куки
        await _signInManager.SignOutAsync();
        return RedirectToAction("List", "BookStore");
    }

    /// <summary>
    /// Форма редактирования профиля.
    /// </summary>
    public async Task<IActionResult> Profile(string? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var user = await _userManager
            .Users
            .FirstOrDefaultAsync(x => x.Id == id);

        return View(user);
    }

    /// <summary>
    /// Редактировать профиль.
    /// </summary>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Profile(string? id, User user)
    {
        if (id != user.Id)
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            return View(user);
        }

        await _userManager.UpdateAsync(user);

        return View(user);
    }
}