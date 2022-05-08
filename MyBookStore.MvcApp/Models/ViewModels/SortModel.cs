using Microsoft.Data.SqlClient;

namespace MyBookStore.MvcApp.Models.ViewModels;

public class SortModel
{
    /// <summary>
    /// Поле сортировки.
    /// </summary>
    public SortBy SortBy { get; set; }

    /// <summary>
    /// Порядок сортировки.
    /// </summary>
    public SortOrder SortOrder { get; set; }
}