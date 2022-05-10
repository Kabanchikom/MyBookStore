using MyBookStore.MvcApp.Infrastructure;
using MyBookStore.MvcApp.Models;
using Newtonsoft.Json;

namespace SportsStore.Models;

public class SessionCart : Cart
{
    [JsonIgnore] public ISession? Session { get; set; }

    public static Cart GetCart(IServiceProvider services)
    {
        var session = services.GetRequiredService<IHttpContextAccessor>()?
            .HttpContext?.Session;
        var cart = session?.GetJson<SessionCart>("Cart")
                   ?? new SessionCart();
        cart.Session = session;
        return cart;
    }

    public override void AddItem(Book book, int quantity)
    {
        base.AddItem(book, quantity);
        Session.SetJson("Cart", this, true);
    }

    public override void RemoveLine(Book book)
    {
        base.RemoveLine(book);
        Session.SetJson("Cart", this, true);
    }

    public override void Clear()
    {
        base.Clear();
        Session?.Remove("Cart");
    }
}