using System.Numerics;

namespace AdventOfCode.Core.Math;

public struct Vector2Int : IEquatable<Vector2Int>, IFormattable
{
    public static Vector2Int Zero = new(0, 0);
    public static Vector2Int One = new(1, 1);
    
    public static Vector2Int Down = new(0, -1);
    public static Vector2Int Left = new(-1, 0);
    public static Vector2Int Right = new(1, 0);
    public static Vector2Int Up = new(0, 1);
    
    public static Vector2Int UpLeft = new(-1, 1);
    public static Vector2Int UpRight = new(1, 1);
    public static Vector2Int DownLeft = new(-1, -1);
    public static Vector2Int DownRight = new(1, -1);
    
    public Vector2Int(int x, int y)
    {
        X = x;
        Y = y;
    }
    
    public int X { get; private set; }
    public int Y { get; private set; }
        
    public static Vector2Int operator -(Vector2Int value) => new(-value.X, -value.Y);
    public static Vector2Int operator -(Vector2Int a, int b) => new(a.X - b, a.Y - b);
    public static Vector2Int operator -(Vector2Int a, Vector2Int b) => new(a.X - b.X, a.Y - b.Y);
    public static Vector2Int operator +(Vector2Int a, Vector2Int b) => new(a.X + b.X, a.Y + b.Y);
    public static Vector2Int operator *(Vector2Int value, int multiplier) => new(value.X * multiplier, value.Y * multiplier);
    public static Vector2Int operator * (Vector2Int a, Vector2Int b) => new(a.X * b.X, a.Y * b.Y);
    public static Vector2Int operator /(Vector2Int value, int divider) => new(value.X / divider, value.Y / divider);
    public static Vector2Int operator /(Vector2Int a, Vector2Int b) => new(a.X / b.X, a.Y / b.Y);

    public static Vector2Int Rotate90Degrees(Vector2Int value, bool rotateClockwise = true)
    {
        var rotationVector = new Vector2Int(0, rotateClockwise ? -1 : 1);
        var newX = value.X * rotationVector.X - value.Y * rotationVector.Y;
        var newY = value.Y * rotationVector.X + value.X * rotationVector.Y;
        return new Vector2Int(newX, newY);
    }
    
    public void Rotate90Degrees(bool rotateClockwise = true)
    {
        var rotationVector = new Vector2Int(0, rotateClockwise ? -1 : 1);
        var newX = X * rotationVector.X - Y * rotationVector.Y;
        var newY = Y * rotationVector.X + X * rotationVector.Y;
        X = newX;
        Y = newY;
    }

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

    public string ToString(string? format, IFormatProvider? formatProvider)
    {
        FormattableString formattable = $"<{X},{Y}>";
        return formattable.ToString(formatProvider);
    }

    public override string ToString()
    {
        return $"<{X},{Y}>";
    }
}