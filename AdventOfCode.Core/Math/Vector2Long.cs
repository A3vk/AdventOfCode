using System.Collections.ObjectModel;

namespace AdventOfCode.Core.Math;

public record struct Vector2Long : IFormattable
{
    public static Vector2Long Zero = new(0, 0);
    public static Vector2Long One = new(1, 1);
    
    public static Vector2Long Down = new(0, -1);
    public static Vector2Long Left = new(-1, 0);
    public static Vector2Long Right = new(1, 0);
    public static Vector2Long Up = new(0, 1);

    public static Vector2Long North = Up;
    public static Vector2Long East = Right;
    public static Vector2Long South = Down;
    public static Vector2Long West = Left;
    
    public static Vector2Long UpLeft = new(-1, 1);
    public static Vector2Long UpRight = new(1, 1);
    public static Vector2Long DownLeft = new(-1, -1);
    public static Vector2Long DownRight = new(1, -1);
    
    public static Vector2Long NorthEast = UpRight;
    public static Vector2Long SouthEast = DownRight;
    public static Vector2Long SouthWest = DownLeft;
    public static Vector2Long NorthWest = UpLeft;

    public static readonly ReadOnlyCollection<Vector2Long> CardinalDirections = new([North, East, South, West]);
    public static readonly ReadOnlyCollection<Vector2Long> OrdinalDirections = new([NorthEast, SouthEast, SouthWest, NorthWest]);
    public static readonly ReadOnlyCollection<Vector2Long> AllDirections = new([..CardinalDirections, ..OrdinalDirections]);
    
    
    public Vector2Long(long x, long y)
    {
        X = x;
        Y = y;
    }
    
    public long X { get; private set; }
    public long Y { get; private set; }
        
    public static Vector2Long operator -(Vector2Long value) => new(-value.X, -value.Y);
    public static Vector2Long operator -(Vector2Long a, long b) => new(a.X - b, a.Y - b);
    public static Vector2Long operator -(Vector2Long a, Vector2Long b) => new(a.X - b.X, a.Y - b.Y);
    public static Vector2Long operator +(Vector2Long a, long b) => new(a.X + b, a.Y + b);
    public static Vector2Long operator +(Vector2Long a, Vector2Long b) => new(a.X + b.X, a.Y + b.Y);
    public static Vector2Long operator *(Vector2Long value, long multiplier) => new(value.X * multiplier, value.Y * multiplier);
    public static Vector2Long operator * (Vector2Long a, Vector2Long b) => new(a.X * b.X, a.Y * b.Y);
    public static Vector2Long operator /(Vector2Long value, long divider) => new(value.X / divider, value.Y / divider);
    public static Vector2Long operator /(Vector2Long a, Vector2Long b) => new(a.X / b.X, a.Y / b.Y);

    public static Vector2Long Rotate90Degrees(Vector2Long value, bool rotateClockwise = true)
    {
        var rotationVector = new Vector2Long(0, rotateClockwise ? -1 : 1);
        var newX = value.X * rotationVector.X - value.Y * rotationVector.Y;
        var newY = value.Y * rotationVector.X + value.X * rotationVector.Y;
        return new Vector2Long(newX, newY);
    }
    
    public void Rotate90Degrees(bool rotateClockwise = true)
    {
        var rotationVector = new Vector2Long(0, rotateClockwise ? -1 : 1);
        var newX = X * rotationVector.X - Y * rotationVector.Y;
        var newY = Y * rotationVector.X + X * rotationVector.Y;
        X = newX;
        Y = newY;
    }
    
    // IFormattable
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