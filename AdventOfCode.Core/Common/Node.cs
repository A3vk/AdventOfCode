using AdventOfCode.Core.Math;

namespace AdventOfCode.Core.Common;

public record Node<T>
{
    public T Value { get; set; }
    public Vector2Int Position { get; set; }

    public Node(T value, Vector2Int position = new())
    {
        Value = value;
        Position = position;
    }
}