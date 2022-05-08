namespace MyBookStore.MvcApp.Models.ViewModels;

public class BookStoreListViewModel
{
    public BookStoreListViewModel(int pageNumber, int pageSize, int itemsCount, List<Book> books)
    {
        PageModel = new PageModel
        {
            ItemsCount = itemsCount,
            PageSize = pageSize,
            CurrentPage = pageNumber
        };

        Books = books;
    }

    public PageModel PageModel { get; set; }
    public List<Book> Books { get; set; }
}