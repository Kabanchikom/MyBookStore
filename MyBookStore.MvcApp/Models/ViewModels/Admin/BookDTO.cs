namespace MyBookStore.MvcApp.Models.ViewModels.Admin;

public class BookDTO
{
    public int Id { get; set; }

    public string Title { get; set; }

    public string? ImagePath { get; set; }

    public DateTime ReceiptDate { get; set; }

    public decimal Price { get; set; }

    public string? Description { get; set; }

    public int ManufacturerId { get; set; }

    public decimal Rating { get; set; }

    public bool IsOnSale { get; set; }

    public decimal Discount { get; set; }

    public List<int> AuthorIds { get; set; } = new();

    public List<int> GenreIds { get; set; } = new();

    public List<int> TypeIds { get; set; } = new();
}