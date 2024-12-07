using System.Numerics;
using AdventOfCode.Core;
using AdventOfCode.Core.Common;
using AdventOfCode.Core.Enums;
using AdventOfCode.Core.Extensions;
using AdventOfCode.Core.Math;

namespace AdventOfCode._2024.Day06;

[ProblemName("Guard Gallivant")]
public class Solution : ISolver
{
    public string PartOne(string input)
    {
        var map = Grid<char>.CreateCharGrid(input);
        var guardPosition = map.GetPosition('^');
        
        ArgumentNullException.ThrowIfNull(guardPosition);
        
        return CountVisitedGuardPositions(map, guardPosition.Value).ToString();
    }

    public string PartTwo(string input)
    {
        throw new NotImplementedException();
    }

    private int CountVisitedGuardPositions(Grid<char> map, Vector2Int guardPosition)
    {
        HashSet<Vector2Int> visited = new HashSet<Vector2Int>();
        
        Direction direction = Direction.Up;
        while (true)
        {
            map[guardPosition.Y][guardPosition.X] = 'X';
            visited.Add(guardPosition);
            
            var nextPosition = guardPosition + direction.ToVector2Int();
            if (map.IsOutOfBounds(nextPosition))
            {
                break;
            }

            // If there is something directly in front of you, turn right 90 degrees.
            if (map[nextPosition.Y][nextPosition.X] == '#')
            {
                direction = direction.Turn90Degrees();
                nextPosition = guardPosition + direction.ToVector2Int();
            }
            
            // Otherwise, take a step forward.
            guardPosition = nextPosition;
            map[guardPosition.Y][guardPosition.X] = GetGuardCharacter(direction);
        }
        
        return visited.Count;
    }

    private char GetGuardCharacter(Direction guardDirection)
    {
        return guardDirection switch
        {
            Direction.Up => '\u2191',
            Direction.UpRight => '\u2197',
            Direction.Right => '\u2192',
            Direction.DownRight => '\u2198',
            Direction.Down => '\u2193',
            Direction.DownLeft => '\u2199',
            Direction.Left => '\u2190',
            Direction.UpLeft => '\u2196',
            _ => throw new ArgumentOutOfRangeException(nameof(guardDirection), guardDirection, null)
        };
    }
}