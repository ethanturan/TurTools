namespace TurTools.Utilities;

public static class EnumerableExtensions
{
    public static IEnumerable<T> WhereNotNull<T>(this IEnumerable<T?> enumerable) where T : struct
    {
        return enumerable.Where(t => t.HasValue).Select(x => x!.Value);
    }
    
    public static IEnumerable<T> WhereNotNull<T>(this IEnumerable<T?> enumerable) where T : class
    {
        return enumerable.Where(t => t is not null)!;
    }
}