using AdventOfCode.Core;
using AdventOfCode.Core.Common;
using AdventOfCode.Core.Math;

namespace AdventOfCode._2024.Day08;

[ProblemName("Resonant Collinearity")]
public class Solution : SolverBase<int, Grid<char>>
{
    protected override int SolvePartOne(string input) => GetUniquePositions(input, GetAntinodesPart1).Count();

    protected override int SolvePartTwo(string input) => GetUniquePositions(input, GetAntinodesPart2).Count();

    private delegate IEnumerable<Vector2Int> GetAntinodes(Grid<char> map, Vector2Int firstAntennaPosition, Vector2Int secondAntennaPosition);
    private IEnumerable<Vector2Int> GetUniquePositions(string input, GetAntinodes getAntinodesFunc)
    {
        var map = Parse(input);

        var antennas = map.Where(x => char.IsAsciiLetterOrDigit(x.Value)).Select(x => x.Position).ToList();
        
        List<Vector2Int> antinodes = [];
        foreach (var sourceAntenna in antennas)
        {
            foreach (var destinationAntenna in antennas)
            {
                if (sourceAntenna == destinationAntenna || map.GetValueOrDefault(sourceAntenna) != map.GetValueOrDefault(destinationAntenna)) continue;
               
                antinodes.AddRange(getAntinodesFunc(map, sourceAntenna, destinationAntenna));
            }
        }
        
        return antinodes.Distinct();
    }
    
    protected override Grid<char> Parse(string input) => Grid<char>.CreateCharGrid(input);

    private IEnumerable<Vector2Int> GetAntinodesPart1(Grid<char> map, Vector2Int firstAntennaPosition, Vector2Int secondAntennaPosition)
    {
        var distance = secondAntennaPosition - firstAntennaPosition;
        var antinodePosition = secondAntennaPosition + distance;
        if (!map.IsOutOfBounds(antinodePosition))
        {
            yield return antinodePosition;
        }
    }
    
    private IEnumerable<Vector2Int> GetAntinodesPart2(Grid<char> map, Vector2Int firstAntennaPosition, Vector2Int secondAntennaPosition)
    {
        var distance = secondAntennaPosition - firstAntennaPosition;
        var antinodePosition = secondAntennaPosition + distance;
        
        while (!map.IsOutOfBounds(antinodePosition))
        {
            yield return antinodePosition;
            antinodePosition += distance;
        }
    }
}