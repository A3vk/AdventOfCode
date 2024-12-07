using System.Numerics;
using AdventOfCode.Core.Math;

namespace AdventOfCode.Core.Enums;

public enum Direction
{
    Up,
    UpRight,
    Right,
    DownRight,
    Down,
    DownLeft,
    Left,
    UpLeft
}

public static class DirectionExtensions
{
    public static Direction Turn90Degrees(this Direction direction, bool turnClockwise = true)
    {
        if (turnClockwise)
        {
            return direction switch
            {
                Direction.Up => Direction.Right,
                Direction.UpRight => Direction.DownRight,
                Direction.Right => Direction.Down,
                Direction.DownRight => Direction.DownLeft,
                Direction.Down => Direction.Left,
                Direction.DownLeft => Direction.UpLeft,
                Direction.Left => Direction.Up,
                Direction.UpLeft => Direction.UpRight,
                _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
            };
        }

        return direction switch
        {
            Direction.Up => Direction.Left,
            Direction.UpRight => Direction.UpLeft,
            Direction.Right => Direction.Up,
            Direction.DownRight => Direction.UpRight,
            Direction.Down => Direction.Right,
            Direction.DownLeft => Direction.DownRight,
            Direction.Left => Direction.Down,
            Direction.UpLeft => Direction.DownLeft,
            _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
        };
    }

    public static Vector2Int ToVector2Int(this Direction direction) =>
        direction switch
        {
            Direction.Up => new Vector2Int(0, -1),
            Direction.UpRight => new Vector2Int(1, -1),
            Direction.Right => new Vector2Int(1, 0),
            Direction.DownRight => new Vector2Int(1, 1),
            Direction.Down => new Vector2Int(0, 1),
            Direction.DownLeft => new Vector2Int(-1, 1),
            Direction.Left => new Vector2Int(-1, 0),
            Direction.UpLeft => new Vector2Int(-1, -1),
            _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
        };
}