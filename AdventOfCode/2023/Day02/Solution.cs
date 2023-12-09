using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;
using AdventOfCode.Core;
using AdventOfCode.Core.Utils;

namespace AdventOfCode._2023.Day02
{
    [ProblemName("Cube Conundrum")]
    public class Solution : ISolver
    {
        public string PartOne(string input)
        {
            var games = input.GetLines().Select(ParseGame);
            return games.Where(g => g is { MaxRed: <= 12, MaxGreen: <= 13, MaxBlue: <= 14 }).Sum(x => x.Id).ToString();
        }

        public string PartTwo(string input)
        {
            var games = input.GetLines().Select(ParseGame);
            return games.Select(g => g.MaxRed * g.MaxGreen * g.MaxBlue).Sum().ToString();
        }

        private static Game ParseGame(string line)
        {
            return new Game(
                ParseInt(line, @"Game (\d+)").First(),
                ParseInt(line, @"(\d+) red").Max(),
                ParseInt(line, @"(\d+) green").Max(),
                ParseInt(line, @"(\d+) blue").Max()
            );
        }

        private static List<int> ParseInt(string value, [StringSyntax(StringSyntaxAttribute.Regex)] string regex)
        {
            var matches = Regex.Matches(value, regex);
            return matches.Select(m => int.Parse(m.Groups[1].Value)).ToList();
        }
    }

    public record Game(int Id, int MaxRed, int MaxGreen, int MaxBlue) {}
}
