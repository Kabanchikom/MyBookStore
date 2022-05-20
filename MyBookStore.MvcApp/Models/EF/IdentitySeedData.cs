using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MyBookStore.MvcApp.Models.EF;

/// <summary>
/// Тестовые данные для пользователей.
/// </summary>
public class IdentitySeedData
{
    public async Task EnsurePopulated(IApplicationBuilder app)
    {
        var context = app.ApplicationServices.GetRequiredService<IdentityContext>();
        var userManager = app.ApplicationServices.GetRequiredService<UserManager<User>>();

        await context.Database.MigrateAsync();

        if (await context.Users.AnyAsync())
        {
            return;
        }

        var users = CreateUsers();

        foreach (var user in users)
        {
            var result = await userManager.CreateAsync(user, "12345");

            if (!result.Succeeded)
            {
                throw new ArgumentException(result.ToString());
            }
        }

        await context.SaveChangesAsync();
    }

    private List<User> CreateUsers()
    {
        var users = new List<User>();
        
        users.Add(new User
        {
            Email = "user@test.com",
            UserName = "user@test.com",
            Surname = "User",
            Name = "User",
            PhoneNumber = "+7-777-777-77-77",
            DateOfBirth = new DateTime(2000, 1, 1),
        });

        return users;
    }
}