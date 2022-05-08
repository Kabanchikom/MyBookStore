namespace MyBookStore.MvcApp.Models.ViewModels;

public class PageModel
{
    public int ItemsCount { get; set; }
    public int PageSize { get; set; }
    public int CurrentPage { get; set; }

    public int PagesCount => (int) Math.Ceiling((decimal) ItemsCount / PageSize);
}