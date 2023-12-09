using System.Text.RegularExpressions;
using AdventOfCode.Core;
using AdventOfCode.Core.Utils;

namespace AdventOfCode._2023.Day06;

[ProblemName("Wait For It")]
public class Solution : ISolver
{
    public string PartOne(string input)
    {
        var races = ParseRaces(input);
        return races.Select(WaysToBeatRecord).Aggregate(1L, (result, value) => result * value).ToString();
    }

    public string PartTwo(string input)
    {
        var races = ParseRaces(input.Replace(" ", ""));
        throw new NotImplementedException();
    }

    private List<Race> ParseRaces(string input)
    {
        var lines = input.GetLines();
        var times = Regex.Matches(lines[0], @"\d+");
        var distances = Regex.Matches(lines[1], @"\d+");

        return times.Zip(distances, (time, distance) => new Race(long.Parse(time.Value), long.Parse(distance.Value))).ToList();
    }

    private long WaysToBeatRecord(Race race)
    {
        // The way to calculate the min and max value is done by solving the quadratic formula `ax^2 + bx + c = 0` where:
        // a = -1
        // b = duration
        // c = -record
        
        // To solve a quadratic formula the following function is used: `x = [ -b plus minus square root of (b^2-4ac) ] / 2a`
        var discriminant = race.Duration * race.Duration - 4 * race.Record;
        var min = (long)Math.Floor((-race.Duration + Math.Sqrt(discriminant)) / -2) + 1;
        var max = (long)Math.Ceiling((-race.Duration - Math.Sqrt(discriminant)) / -2) - 1;
        return max - min + 1;
    }
}

public record Race(long Duration, long Record);