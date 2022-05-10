using Microsoft.EntityFrameworkCore;
using MyBookStore.MvcApp.Infrastructure;
using MyBookStore.MvcApp.Models.EF;
using SportsStore.Models;

var builder = WebApplication.CreateBuilder(args);

var connection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Host.UseDefaultServiceProvider(options => options.ValidateScopes = false);
builder.Services.AddControllersWithViews();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();
builder.Services.AddDbContext<BookStoreContext>(options => options.UseSqlServer(connection));
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
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=BookStore}/{action=List}/{id?}");

var seedData = new SeedData();
await seedData.EnsurePopulated(app, builder);

app.Run();