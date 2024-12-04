namespace AdventOfCode.Core.Math;

public struct Vector2Int : IEquatable<Vector2Int>
{
    public static Vector2Int Down = new(0, -1);
    public static Vector2Int Left = new(-1, 0);
    public static Vector2Int Right = new(1, 0);
    public static Vector2Int Up = new(0, 1);
    
    public static Vector2Int Zero = new(0, 0);
    public static Vector2Int One = new(0, 0);
    
    public Vector2Int(int x, int y)
    {
        X = x;
        Y = y;
    }
    
    public int X { get; }
    public int Y { get; }
        
    public static Vector2Int operator -(Vector2Int a, Vector2Int b) => new(a.X - b.X, a.Y - b.Y);
    public static Vector2Int operator +(Vector2Int a, Vector2Int b) => new(a.X + b.X, a.Y + b.Y);

    public bool Equals(Vector2Int other)
    {
        return X == other.X && Y == other.Y;
    }

    public override bool Equals(object? obj)
    {
        return obj is Vector2Int other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y);
    }
}