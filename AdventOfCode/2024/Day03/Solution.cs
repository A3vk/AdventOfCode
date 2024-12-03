using System.Text.RegularExpressions;
using AdventOfCode.Core;
using AdventOfCode.Core.Extensions;

namespace AdventOfCode._2024.Day03;

[ProblemName("Mull It Over")]
public class Solution : ISolver
{
    public string PartOne(string input)
    {
        var expressions = GetMultiplicationExpressions(input);
        return expressions.Sum(exp => exp.X * exp.Y).ToString();
    }

    public string PartTwo(string input)
    {
        throw new NotImplementedException();
    }

    private List<MultiplicationExpression> GetMultiplicationExpressions(string input)
    {
        var matches = Regex.Matches(input, @"mul\((\d+),(\d+)\)");
        return matches.Select(match => new MultiplicationExpression { X = int.Parse(match.Groups[1].Value), Y = int.Parse(match.Groups[2].Value) }).ToList();
    }

    private struct MultiplicationExpression
    {
        public int X { get; init; }
        public int Y { get; init; }
    }
}