namespace AdventOfCode.Core.Extensions;

public static class StringExtensions
{
    public static List<string> GetLines(this string value)
    {
        return value.NormalizeBreaks().Split('\n').ToList();
    }

    public static string NormalizeBreaks(this string value) => value.Replace("\r", "");

    public static List<int> ToIntList(this string value, string separator = ",")
    {
        return value.Split(separator).Select(int.Parse).ToList();
    }
}