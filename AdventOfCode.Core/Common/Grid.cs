using System.Collections;
using System.Text;
using AdventOfCode.Core.Extensions;
using AdventOfCode.Core.Math;

namespace AdventOfCode.Core.Common;

public class Grid<T> : IEnumerable<Node<T>>
{
    private readonly Node<T>[,] _grid;
    public Vector2Int Size { get; private set; }
    
    public static Grid<char> CreateCharGrid(string input)
    {
        return new Grid<char>(input.GetLines().Select(x => x.ToCharArray().ToList()).ToList());
    }
    
    public Grid(IEnumerable<IEnumerable<T>> grid)
    {
        Size = new Vector2Int(grid.First().Count(), grid.Count());
        _grid = new Node<T>[Size.X, Size.Y];

        for (int y = 0; y < Size.Y; y++)
        {
            for (int x = 0; x < Size.X; x++)
            {
                var value = grid.ElementAt(Size.Y - y - 1).ElementAt(x);
                _grid[x, y] = new Node<T>(value, new Vector2Int(x, y));
            }
        }
    }

    private Grid(Node<T>[,] grid, Vector2Int size)
    {
        _grid = grid;
        Size = size;
    }

    public Vector2Int? GetPosition(T value)
    {
        for (int y = 0; y < Size.Y; y++)
        {
            for (int x = 0; x < Size.X; x++)
            {
                if (_grid[x, y].Value?.Equals(value) == true)
                {
                    return new Vector2Int(x, y);
                }
            }
        }

        return null;
    }

    public Grid<T>? CreateSubGrid(Vector2Int bottomLeftPosition, Vector2Int size)
    {
        
        var topRightPosition = bottomLeftPosition + (size - 1);
        if (IsOutOfBounds(bottomLeftPosition) || IsOutOfBounds(topRightPosition))
        {
            return null;
        }

        var subGrid = new Node<T>[size.X, size.Y];
            
        for (int y = 0; y < size.Y; y++)
        {
            for (int x = 0; x < size.X; x++)
            {
                subGrid[x,y] = new Node<T>(_grid[bottomLeftPosition.X + x, bottomLeftPosition.Y + y].Value, new Vector2Int(x, y));
            }
        }
        
        return new Grid<T>(subGrid, size);;
    }

    public T? GetValueOrDefault(Vector2Int position)
    {
        return !IsOutOfBounds(position) ? _grid[position.X, position.Y].Value : default;
    }
    
    public Node<T>? GetNodeOrDefault(Vector2Int position)
    {
        return !IsOutOfBounds(position) ? _grid[position.X, position.Y] : default;
    }
    
    public Node<T> GetNode(Vector2Int position)
    {
        return !IsOutOfBounds(position) ? _grid[position.X, position.Y] : throw new ArgumentOutOfRangeException(nameof(position));
    }
    
    public T? GetValueOrDefault(int x, int y)
    {
        var position = new Vector2Int(x, y);
        return GetValueOrDefault(position);
    }

    public void SetValue(Vector2Int position, T value)
    {
        if (IsOutOfBounds(position)) throw new IndexOutOfRangeException("Position is out of bounds");
        
        _grid[position.X, position.Y] = new Node<T>(value, position);
    }

    public List<Node<T>> GetNeighbors(Vector2Int position, bool includeDiagonals = false)
    {
        List<Node<T>> neighbors = [];
        foreach (var direction in Vector2Int.AllDirections)
        {
            if (!includeDiagonals && !(direction.X == 0 || direction.Y == 0)) continue;

            var neighborPosition = position + direction;
            if (!IsOutOfBounds(neighborPosition))
            {
                neighbors.Add(_grid[neighborPosition.X, neighborPosition.Y]);
            }
        }
        
        return neighbors;
    }

    public bool IsOutOfBounds(Vector2Int position) =>  position.X < 0 || position.X >= Size.X || position.Y < 0 || position.Y >= Size.Y;

    public IEnumerator<Node<T>> GetEnumerator()
    {
        for (int y = 0; y < Size.Y; y++)
        {
            for (int x = 0; x < Size.X; x++)
            {
                yield return _grid[x, y];
            }
        }
    }

    public override string ToString()
    {
        StringBuilder builder = new();
        for (int y = Size.Y - 1; y >= 0; y--)
        {
            for (int x = 0; x < Size.X; x++)
            {
                builder.Append(_grid[x, y].Value);
            }
            builder.AppendLine();
        }
        return builder.ToString();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}