using System.Text.RegularExpressions;
using AdventOfCode.Core;
using AdventOfCode.Core.Utils;

namespace AdventOfCode._2023.Day08;

using Map = Dictionary<string, (string Left, string Right)>;

[ProblemName("Haunted Wasteland")]
public class Solution : ISolver
{
    public string PartOne(string input)
    {
        var blocks = input.NormalizeBreaks().Split("\n\n");
        var directions = blocks[0];
        var map = ParseMap(blocks[1].GetLines());

        return NumberOfStepsToEnd("AAA", "ZZZ", directions, map).ToString();
    }

    public string PartTwo(string input)
    {
        var blocks = input.NormalizeBreaks().Split("\n\n");
        var directions = blocks[0];
        var map = ParseMap(blocks[1].GetLines());

        return map.Keys
            .Where(key => key.EndsWith('A'))
            .Select(key => NumberOfStepsToEnd(key, "Z", directions, map))
            .Aggregate(1L, CustomMath.LeastCommonMultiple).ToString();
    }
    
    

    private long NumberOfStepsToEnd(string start, string endMarker, string directions, Map map)
    {
        var current = start;
        var steps = 0;
        while (!current.EndsWith(endMarker))
        {
            var direction = directions[steps % directions.Length];
            current = direction == 'L' ? map[current].Left : map[current].Right;
            
            steps++;
        }

        return steps;
    }

    private Map ParseMap(List<string> lines)
    {
        return lines.Select(line => Regex.Matches(line, "[A-Z]{3}")).ToDictionary(m => m[0].Value, m => (m[1].Value, m[2].Value));
    }
}