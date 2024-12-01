using System.Text.RegularExpressions;
using AdventOfCode.Core;
using AdventOfCode.Core.Extensions;

namespace AdventOfCode._2023.Day01
{
    [ProblemName("Trebuchet?!")]
    public class Solution : ISolver
    {
        public string PartOne(string input)
        {
            return Solve(input, @"\d").ToString();
        }

        public string PartTwo(string input)
        {
            return Solve(input, @"\d|one|two|three|four|five|six|seven|eight|nine").ToString();
        }

        private int Solve(string input, string regex)
        {
            var result = 0;
            var lines = input.GetLines();
            foreach (var line in lines)
            {
                var firstMatch = Regex.Match(line, regex);
                var lastMatch = Regex.Match(line, regex, RegexOptions.RightToLeft);
                result += ParseMatch(firstMatch) * 10 + ParseMatch(lastMatch);
            }

            return result;
        }
        
        private static int ParseMatch(Capture match)
        {
            return match.Value switch
            {
                "one" => 1,
                "two" => 2,
                "three" => 3,
                "four" => 4,
                "five" => 5,
                "six" => 6,
                "seven" => 7,
                "eight" => 8,
                "nine" => 9,
                var digit => int.Parse(digit)
            };
        }
    }
}
