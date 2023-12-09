using System.Text.RegularExpressions;
using AdventOfCode.Core;
using AdventOfCode.Core.Utils;

namespace AdventOfCode._2023.Day05;

[ProblemName("If You Give A Seed A Fertilizer")]
public class Solution : ISolver
{
    public string PartOne(string input)
    {
        var almanac = ParseAlmanac(input.NormalizeBreaks());
        return almanac.Seeds.Select(seed => almanac.MapSeedToLocation(seed)).Min().ToString();
    }

    public string PartTwo(string input)
    {
        var almanac = ParseAlmanac(input.NormalizeBreaks());
        var seedRanges = almanac.Seeds.Chunk(2).Select(x => Range.CreateRangeFromSize(x[0], x[1])).ToList();
        return seedRanges.SelectMany(seedRange => almanac.MapSeedToLocation(seedRange)).Select(x => x.Start).Min().ToString();
    }

    private Almanac ParseAlmanac(string input)
    {
        var blocks = input.Split("\n\n");
        var seeds = Regex.Matches(blocks[0], @"\d+").Select(match => long.Parse(match.Value)).ToList();
        var maps = blocks[1..].Select(ParseMap).ToList();
        return new Almanac(seeds, maps);
    }

    private Map ParseMap(string map)
    {
        var mapEntries = map.GetLines()[1..].Select(ParseMapEntry).ToList();
        return new Map(mapEntries);
    }

    private MapEntry ParseMapEntry(string mapEntry)
    {
        var numbers = Regex.Matches(mapEntry, @"\d+").Select(match => long.Parse(match.Value)).ToList();
        return new MapEntry(Range.CreateRangeFromSize(numbers[1], numbers[2]), Range.CreateRangeFromSize(numbers[0], numbers[2]));
    }
}

public record Almanac(List<long> Seeds, List<Map> Maps)
{
    public long MapSeedToLocation(long seed)
    {
        return Maps.Aggregate(seed, (current, map) => map.MapEntries.FirstOrDefault(entry => entry.Source.Contains(current))?.Map(current) ?? current);
    }
    
    public List<Range> MapSeedToLocation(Range seed)
    {
        var results = new List<Range>() { seed };
        foreach (var map in Maps)
        {
            var queue = new Queue<Range>(results);
            results.Clear();

            while (queue.Count != 0)
            {
                var currentRange = queue.Dequeue();
                var mapEntry = map.MapEntries.FirstOrDefault(entry => entry.Source.Intersects(currentRange));
                if (mapEntry == null)
                {
                    results.Add(currentRange);
                }
                else if (mapEntry.Source.Start <= currentRange.Start && mapEntry.Source.End >= currentRange.End)
                {
                    var diff = mapEntry.Destination.Start - mapEntry.Source.Start ;
                    results.Add(new Range(currentRange.Start + diff, currentRange.End + diff));
                }
                else if (mapEntry.Source.Start > currentRange.Start)
                {
                    queue.Enqueue(currentRange with { End = mapEntry.Source.Start - 1 });
                    queue.Enqueue(currentRange with { Start = mapEntry.Source.Start });
                }
                else
                {
                    queue.Enqueue(currentRange with { End = mapEntry.Source.End });
                    queue.Enqueue(currentRange with { Start = mapEntry.Source.End + 1 });
                }
            }
        }
        return results;
    }
}

public record Map(List<MapEntry> MapEntries);

public record MapEntry(Range Source, Range Destination)
{
    public long Map(long value)
    {
        var diff = value - Source.Start;
        return Destination.Start + diff;
    }
}

public record Range(long Start, long End)
{
    public bool Contains(long value)
    {
        return Start <= value && End >= value;
    }

    public bool Intersects(Range other)
    {
        return Start <= other.End && End >= other.Start;
    }

    public static Range CreateRangeFromSize(long start, long size)
    {
        return new Range(start, start + size - 1);
    }
}