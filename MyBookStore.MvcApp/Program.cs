using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyBookStore.MvcApp.Infrastructure;
using MyBookStore.MvcApp.Models;
using MyBookStore.MvcApp.Models.EF;
using SportsStore.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<IdentityContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection")));

builder.Services.AddDbContext<BookStoreContext>(opts =>
    opts.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<User, IdentityRole>(options =>
    {
        options.SignIn.RequireConfirmedAccount = false;

        options.Password = new PasswordOptions
        {
            RequireDigit = false,
            RequiredLength = 5,
            RequireLowercase = false,
            RequireUppercase = false,
            RequireNonAlphanumeric = false
        };
    })
    .AddEntityFrameworkStores<IdentityContext>();

builder.Host.UseDefaultServiceProvider(options => options.ValidateScopes = false);
builder.Services.AddControllersWithViews();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.LogoutPath = "/Account/Logout";
});
builder.Services.AddAutoMapper(typeof(AppMappingProfile));
builder.Services.AddScoped(SessionCart.GetCart);
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=BookStore}/{action=List}/{id?}");

var domainSeedData = new DomainSeedData();
var identitySeedData = new IdentitySeedData();

await identitySeedData.EnsurePopulated(app);
await domainSeedData.EnsurePopulated(app, builder);

app.Run();