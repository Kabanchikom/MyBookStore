using Microsoft.EntityFrameworkCore;

namespace MyBookStore.MvcApp.Models.EF;

public class BookStoreContext : DbContext
{
    public DbSet<Book> Books { get; set; }
    public DbSet<BookType> BookTypes { get; set; }
    public DbSet<DeliveryType> DeliveryTypes { get; set; }
    public DbSet<Manufacturer> Manufacturers { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderStatus> OrderStatuses { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Genre> Genres { get; set; }

    public BookStoreContext(DbContextOptions<BookStoreContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>().HasKey(x => x.Id);
        modelBuilder.Entity<BookType>().HasKey(x => x.Id);
        modelBuilder.Entity<DeliveryType>().HasKey(x => x.Id);
        modelBuilder.Entity<Manufacturer>().HasKey(x => x.Id);
        modelBuilder.Entity<Order>().HasKey(x => x.Id);
        modelBuilder.Entity<OrderStatus>().HasKey(x => x.Id);
        modelBuilder.Entity<Review>().HasKey(x => x.Id);
        modelBuilder.Entity<Author>().HasKey(x => x.Id);
        modelBuilder.Entity<Genre>().HasKey(x => x.Id);

        modelBuilder.Entity<Book>()
            .Property(x => x.Price)
            .HasPrecision(14, 2);

        modelBuilder.Entity<Book>()
            .Property(x => x.Rating)
            .HasPrecision(3, 2);

        modelBuilder.Entity<Book>()
            .Property(x => x.Discount)
            .HasPrecision(3, 2);

        modelBuilder.Entity<Book>()
            .HasMany(x => x.Types)
            .WithMany(x => x.Books);

        modelBuilder.Entity<Book>()
            .HasOne(x => x.Manufacturer)
            .WithMany(x => x.Books);

        modelBuilder.Entity<Book>()
            .HasMany(x => x.Authors)
            .WithMany(x => x.Books);

        modelBuilder.Entity<Book>()
            .HasMany(x => x.Genres)
            .WithMany(x => x.Books);

        modelBuilder.Entity<Order>()
            .HasOne(x => x.OrderStatus)
            .WithMany(x => x.Orders);

        modelBuilder.Entity<Review>()
            .HasOne(x => x.Book)
            .WithMany(x => x.Reviews);
    }
}