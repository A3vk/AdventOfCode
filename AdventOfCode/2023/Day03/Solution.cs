using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;
using AdventOfCode.Core;
using AdventOfCode.Core.Utils;

namespace AdventOfCode._2023.Day03
{
    [ProblemName("Gear Ratios")]
    public class Solution : ISolver
    {
        public string PartOne(string input)
        {
            var rows = input.GetLines();
            var parts = ParseComponents(rows, @"\d+");
            var symbols = ParseComponents(rows, @"[^.0-9]");

            var adjacentParts = parts.Where(part => symbols.Any(symbol => IsAdjacent(part, symbol)));
            return adjacentParts.Sum(x => x.IntValue).ToString();
        }

        public string PartTwo(string input)
        {
            var rows = input.GetLines();
            var parts = ParseComponents(rows, @"\d+");
            var gears = ParseComponents(rows, @"\*");
            
            return gears.Select(gear => parts.Where(part => IsAdjacent(part, gear)).ToList())
                .Where(neighbours => neighbours.Count == 2)
                .Sum(neighbours => neighbours.First().IntValue * neighbours.Last().IntValue)
                .ToString();
        }

        private List<Component> ParseComponents(List<string> rows, [StringSyntax(StringSyntaxAttribute.Regex)] string regex)
        {
            var result = new List<Component>();
            for (int i = 0; i < rows.Count; i++)
            {
                result.AddRange(Regex.Matches(rows[i], regex).Select(m => new Component(m.Value, i, m.Index)));
            }
            return result;
        }

        private static bool IsAdjacent(Component component1, Component component2)
        {
            return Math.Abs(component1.RowIndex - component2.RowIndex) <= 1 && 
                   component1.ColIndex <= component2.ColIndex + component2.Value.Length &&
                   component1.ColIndex + component1.Value.Length >= component2.ColIndex;
        }
    }

    public record Component(string Value, int RowIndex, int ColIndex)
    {
        public int IntValue => int.Parse(Value);
    }
}
