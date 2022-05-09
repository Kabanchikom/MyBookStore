namespace MyBookStore.MvcApp.Models.ViewModels;

public class BooksFilterModel
{
    public List<Manufacturer> Manufacturers { get; set; } = new();
    public Manufacturer? SelectedManufacturer { get; set; } = new();
}