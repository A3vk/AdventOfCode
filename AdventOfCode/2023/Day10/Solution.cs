using AdventOfCode.Core;
using AdventOfCode.Core.Extensions;

namespace AdventOfCode._2023.Day10;

[ProblemName("Pipe Maze")]
public class Solution : ISolver
{
    private static readonly Point Up = new Point(0, -1);
    private static readonly Point Down = new Point(0, 1);
    private static readonly Point Left = new Point(-1, 0);
    private static readonly Point Right = new Point(1, 0);
    private static readonly Point[] Directions = [Up, Down, Left, Right];
    
    private readonly Dictionary<char, Pipe> _pipeDictionary = new()
    {
        { '|', new Pipe(Up, Down) },
        { '-', new Pipe(Left, Right) },
        { 'L', new Pipe(Up, Right) },
        { 'J', new Pipe(Up, Left) },
        { '7', new Pipe(Left, Down) },
        { 'F', new Pipe(Right, Down) }
    };
    
    public string PartOne(string input)
    {
        var map = new Map(input);
        var loop = FindLoop(map);

        return (loop.Count / 2).ToString();
    }

    public string PartTwo(string input)
    {
        var map = new Map(input);
        var loop = FindLoop(map);
        return map.GetCellList().Count(cell => IsInside(cell.point, map, loop)).ToString();
    }
    
    private List<Point> FindLoop(Map map)
    {
        var loop = new List<Point>();

        var position = map.Start;
        var direction = Directions.First(dir => _pipeDictionary.TryGetValue(map[position + dir], out var pipe) && pipe.ConnectsTo(-dir));

        while (true)
        {
            loop.Add(position);
            position += direction;
            
            if (position == map.Start) break;
            
            direction = _pipeDictionary[map[position]].GetOppositeConnections(-direction);
        }

        return loop;
    }

    private bool IsInside(Point position, Map map, List<Point> loop)
    {
        if (loop.Contains(position)) return false;
        
        // Find direction to closest edge
        // Check how many times a perpendicular part of the loop is crossed

        int crossedLoop = 0;
        position += Left;
        
        while (map.Contains(position))
        {
            if (loop.Contains(position) && _pipeDictionary.TryGetValue(map[position], out var pipe) && (pipe.ConnectsTo(Up)))
            {
                crossedLoop++;
            }
            position += Left;
        }

        return int.IsOddInteger(crossedLoop);
    }

    private class Map
    {
        public int Width { get; init; }
        public int Height { get; init; }
        public Point Start { get; init; }
        public char[][] Cells { get; init; }

        public char this[Point point]
        {
            get => Cells[point.Y][point.X];
            set => Cells[point.Y][point.X] = value;
        }

        public Map(string input)
        {
            Cells = input.GetLines().Select(s => s.ToCharArray()).ToArray();
            Width = Cells.First().Length;
            Height = Cells.Length;
            Start = GetCellList().Where(cell => cell.value == 'S').Select(cell => cell.point).First();
        }

        public List<(Point point, char value)> GetCellList() => Cells.SelectMany((row, y) => row.Select((value, x) => (new Point(x, y), value))).ToList();

        public bool Contains(Point point) => point.X >= 0 && point.X < Width && point.Y >= 0 && point.Y < Height;
    }

    private record Pipe(Point From, Point To)
    {
        public Pipe((int x, int y) from, (int x, int y) to) : this(new Point(from.x, from.y), new Point(to.x, to.y)) { }

        public bool ConnectsTo(Point direction) => From == direction || To == direction;

        public Point GetOppositeConnections(Point direction) => direction == From ? To : From;
    }

    private record Point(int X, int Y)
    {
        public static Point operator +(Point a, Point b) => new(a.X + b.X, a.Y + b.Y);
        public static Point operator -(Point a) => new(-a.X, -a.Y);
    }
}