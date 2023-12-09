namespace AdventOfCode.Core.Utils;

public static class StringExtensions
{
    public static List<string> GetLines(this string value)
    {
        return value.NormalizeBreaks().Split('\n').ToList();
    }

    public static string NormalizeBreaks(this string value) => value.Replace("\r", "");
}