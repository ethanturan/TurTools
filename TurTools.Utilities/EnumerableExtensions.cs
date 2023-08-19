namespace TurTools.Utilities;

/// <summary>
/// Extensions on IEnumerable
/// </summary>
public static class EnumerableExtensions
{
    /// <summary>
    /// Removes null values and applies a null assertion to T
    /// </summary>
    /// <param name="source">An <see cref="IEnumerable{T}"/> to filter null values from</param>
    /// <typeparam name="T">The type of elements in source</typeparam>
    /// <returns>An <see cref="IEnumerable{T}"/> result that contains no null values or references</returns>
    public static IEnumerable<T> WhereNotNull<T>(this IEnumerable<T?> source) where T : struct
    {
        return source.Where(t => t.HasValue).Select(x => x!.Value);
    }
    
    /// <inheritdoc cref="WhereNotNull{T}(System.Collections.Generic.IEnumerable{System.Nullable{T}})"/>
    public static IEnumerable<T> WhereNotNull<T>(this IEnumerable<T?> source) where T : class
    {
        return source.Where(t => t is not null)!;
    }
}