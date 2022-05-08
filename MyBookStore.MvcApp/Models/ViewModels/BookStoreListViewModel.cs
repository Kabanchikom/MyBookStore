using Microsoft.Data.SqlClient;

namespace MyBookStore.MvcApp.Models.ViewModels;

public class BookStoreListViewModel
{
    public BookStoreListViewModel(
        int pageNumber,
        int pageSize,
        int itemsCount,
        SortOrder sortOrder,
        SortBy sortBy,
        List<Book> books)
    {
        PageModel = new PageModel
        {
            ItemsCount = itemsCount,
            PageSize = pageSize,
            CurrentPage = pageNumber
        };

        SortModel = new SortModel
        {
            SortOrder = sortOrder,
            SortBy = sortBy
        };

        Books = books;
    }

    public PageModel PageModel { get; set; }

    public SortModel SortModel { get; set; }

    public List<Book> Books { get; set; }
}