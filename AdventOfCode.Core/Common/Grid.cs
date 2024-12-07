using System.Text;
using AdventOfCode.Core.Extensions;
using AdventOfCode.Core.Math;

namespace AdventOfCode.Core.Common;

public class Grid<T> : List<List<T>>
{
    private Vector2Int _size;
    
    public static Grid<char> CreateCharGrid(string input)
    {
        return new Grid<char>(input.GetLines().Select(x => x.ToCharArray().ToList()).ToList());
    }
    
    public Grid(List<List<T>> grid) : base(grid)
    {
        _size = new Vector2Int(grid[0].Count, grid.Count);
    }

    public Vector2Int? GetPosition(T value)
    {
        for (int y = 0; y < Count; y++)
        {
            for (int x = 0; x < this[y].Count; x++)
            {
                if (this[y][x]!.Equals(value))
                {
                    return new Vector2Int(x, y);
                }
            }
        }

        return null;
    }

    public bool IsOutOfBounds(Vector2Int position) =>  position.X < 0 || position.X >= _size.X || position.Y < 0 || position.Y >= _size.Y;

    public override string ToString()
    {
        StringBuilder builder = new();
        for (int y = 0; y < Count; y++)
        {
            for (int x = 0; x < this[y].Count; x++)
            {
                builder.Append(this[y][x]);
            }
            builder.AppendLine();
        }
        return builder.ToString();
    }
}