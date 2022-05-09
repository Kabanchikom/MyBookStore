using Microsoft.Data.SqlClient;

namespace MyBookStore.MvcApp.Models.ViewModels;

public class BookStoreListViewModel
{
    public BookStoreListViewModel(
        string searchText,
        int pageNumber,
        int pageSize,
        int itemsCount,
        int? manufacturerId,
        SortOrder sortOrder,
        SortBy sortBy,
        List<Book> books,
        List<Manufacturer> manufacturers)
    {
        SearchText = searchText;

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

        FilterModel = new BooksFilterModel
        {
            Manufacturers = manufacturers,
            SelectedManufacturer = manufacturers
                .FirstOrDefault(x => x.Id == manufacturerId)
        };

        Books = books;
    }

    public PageModel PageModel { get; set; }

    public SortModel SortModel { get; set; }

    public BooksFilterModel FilterModel { get; set; }

    public List<Book> Books { get; set; }

    public string SearchText { get; set; }
}