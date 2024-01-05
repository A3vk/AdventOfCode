using AdventOfCode.Core;
using AdventOfCode.Core.Utils;

namespace AdventOfCode._2023.Day09;

[ProblemName("Mirage Maintenance")]
public class Solution : ISolver
{
    public string PartOne(string input)
    {
        var lines = input.GetLines().Select(line => line.Split(' ').Select(long.Parse).ToList()).ToList();
        return lines.Select(Extrapolate).Sum().ToString();
    }

    public string PartTwo(string input)
    {
        var lines = input.GetLines().Select(line => line.Split(' ').Select(long.Parse).ToList()).ToList();
        return lines.Select(x => Extrapolate(x.ToArray().Reverse().ToList())).Sum().ToString();
    }

    private long Extrapolate(List<long> numbers) => numbers.Count == 0 ? 0 : Extrapolate(Diff(numbers)) + numbers.Last();

    private List<long> Diff(List<long> numbers) => numbers.Zip(numbers.Skip(1)).Select(x => x.Second - x.First).ToList();
}