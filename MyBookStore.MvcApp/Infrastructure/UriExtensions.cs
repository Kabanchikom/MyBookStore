namespace MyBookStore.MvcApp.Infrastructure;

public static class UrlExtensions
{
    /// <summary>
    /// Возвращает строку запроса. Если есть параметры, возвращает строку с параметрами.
    /// </summary>
    public static string PathAndQuery(this HttpRequest request) =>
        request.QueryString.HasValue
            ? $"{request.Path}{request.QueryString}"
            : request.Path.ToString();
}