namespace AdventOfCode.Core.Utils;

public static class EnumerableExtensions
{
    public static IEnumerable<TResult> SelectTwo<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TSource, TResult> selector)
    {
        return source.Zip(source.Skip(1), selector);
    }
}