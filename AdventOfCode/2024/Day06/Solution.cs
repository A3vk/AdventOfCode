using AdventOfCode.Core;
using AdventOfCode.Core.Common;
using AdventOfCode.Core.Math;

namespace AdventOfCode._2024.Day06;

[ProblemName("Guard Gallivant")]
public class Solution : ISolver
{
    public string PartOne(string input)
    {
        var (map, guardPosition) = Parse(input);
        
        return Walk(map, guardPosition).positions.Count().ToString();
    }

    public string PartTwo(string input)
    {
        var (map, guardPosition) = Parse(input);
        
        var positions = Walk(map, guardPosition).positions;
        int numberOfLoops = 0;
        foreach (var position in positions)
        {
            map.SetValue(position, '#');
            if (Walk(map, guardPosition).isLoop)
            {
                numberOfLoops++;
            }
            map.SetValue(position, '.');
        }

        return numberOfLoops.ToString();
    }

    private (IEnumerable<Vector2Int> positions, bool isLoop) Walk(Grid<char> map, Vector2Int guardPosition)
    {
        HashSet<(Vector2Int position, Vector2Int direction)> visited = [];
        
        var direction = Vector2Int.Up;
        while (!map.IsOutOfBounds(guardPosition) && !visited.Contains((guardPosition, direction)))
        {
            visited.Add((guardPosition, direction));
            if (map.GetValueOrDefault(guardPosition + direction) == '#')
            {
                direction.Rotate90Degrees();
            }
            else
            {
                guardPosition += direction;
            }
        }
        
        return (
            positions: visited.Select(x => x.position).Distinct(), 
            isLoop: visited.Contains((guardPosition, direction))
        );
    }

    private (Grid<char> map, Vector2Int guardPosition) Parse(string input)
    {
        var map = Grid<char>.CreateCharGrid(input);
        var guardPosition = map.GetPosition('^');
        ArgumentNullException.ThrowIfNull(guardPosition);
        
        return (map, guardPosition.Value);
    }
}