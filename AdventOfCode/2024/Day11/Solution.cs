using System.Collections.Concurrent;
using AdventOfCode.Core;

namespace AdventOfCode._2024.Day11;

using Cache = ConcurrentDictionary<(long stone, int blinks), long>;

[ProblemName("Plutonian Pebbles")]
public class Solution : SolverBase<long, List<long>>
{
    protected override long SolvePartOne(List<long> input) => CountStones(input, 25);
    protected override long SolvePartTwo(List<long> input) => CountStones(input, 75);

    protected override List<long> Parse(string input) => input.Split(" ").Select(long.Parse).ToList();
    
    private long CountStones(List<long> stones, int numberOfBlinks)
    {
        var cache = new Cache();
        return stones.Sum(x => Blink(x, numberOfBlinks, cache));
    }
    
    private long Blink(long stone, int remainingBlinks, Cache cache)
    {
        return cache.GetOrAdd((stone, remainingBlinks), key =>
        {
            var stoneString = key.stone.ToString();
            
            if (key.blinks == 0) return 1;
            
            // If the stone is engraved with the number 0, it is replaced by a stone engraved with the number 1.
            if (stone == 0)
            {
                return Blink(1, key.blinks - 1, cache);
            }
            // If the stone is engraved with a number that has an even number of digits, it is replaced by two stones. The left half of the digits are engraved on the new left stone, and the right half
            // of the digits are engraved on the new right stone. (The new numbers don't keep extra leading zeroes: 1000 would become stones 10 and 0.)
            if (stoneString.Length % 2 == 0)
            {
                return Blink(long.Parse(stoneString[(stoneString.Length / 2)..]), key.blinks - 1, cache) + Blink(long.Parse(stoneString[..(stoneString.Length / 2)]), key.blinks - 1, cache);
            }

            // If none of the other rules apply, the stone is replaced by a new stone; the old stone's number multiplied by 2024 is engraved on the new stone.
            return Blink(stone * 2024, key.blinks - 1, cache);
        });
    }
}