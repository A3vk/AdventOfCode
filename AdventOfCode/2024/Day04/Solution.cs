using System.Numerics;
using AdventOfCode.Core;
using AdventOfCode.Core.Extensions;
using AdventOfCode.Core.Math;

namespace AdventOfCode._2024.Day04;

[ProblemName("Ceres Search")]
public class Solution : ISolver
{
    public string PartOne(string input)
    {
        var wordGrid = new WordGrid(input);
        return wordGrid.CountOccurrencesInGrid("XMAS").ToString();
    }

    public string PartTwo(string input)
    {
        throw new NotImplementedException();
    }

    private class WordGrid
    {
        private readonly char[][] _grid;

        private readonly Dictionary<Direction, Vector2Int> _directions = new()
        {
            { Direction.Up, new Vector2Int(0, -1) },
            { Direction.UpRight, new Vector2Int(1, -1) },
            { Direction.Right, new Vector2Int(1, 0) },
            { Direction.DownRight, new Vector2Int(1, 1) },
            { Direction.Down, new Vector2Int(0, 1) },
            { Direction.DownLeft, new Vector2Int(-1, 1) },
            { Direction.Left, new Vector2Int(-1, 0) },
            { Direction.UpLeft, new Vector2Int(-1, -1) }
        };
        
        public WordGrid(string input)
        {
            var lines = input.GetLines();
            _grid = lines.Select(x => x.ToCharArray()).ToArray();
        }

        public int CountOccurrencesInGrid(string word)
        {
            int result = 0;

            char firstLetter = word[0];
            string neededRemainingWord = word[1..];

            for (int y = 0; y < _grid.Length; y++)
            {
                for (int x = 0; x < _grid[y].Length; x++)
                {
                    if (_grid[y][x] != firstLetter) continue;

                    foreach (var direction in Enum.GetValues<Direction>())
                    {
                        var remainder = GetLettersInDirection(direction, new Vector2Int(x, y), neededRemainingWord.Length);
                        if (remainder == neededRemainingWord) { result += 1; }
                    }
                }
            }

            return result;
        }

        private string GetLettersInDirection(Direction direction, Vector2Int position, int length)
        {
            char[] letters = new char[length];
            
            Vector2Int currentPosition = position + _directions[direction];
            for (int i = 0; i < length; i++)
            {
                if (currentPosition.Y < 0 || currentPosition.Y >= _grid.Length || currentPosition.X < 0 || currentPosition.X >= _grid[currentPosition.Y].Length) break;
                
                letters[i] = _grid[currentPosition.Y][currentPosition.X];
                
                currentPosition += _directions[direction];
            }
            
            return new string(letters);
        }
    }

    private enum Direction
    {
        Up, 
        UpRight,
        Right,
        DownRight,
        Down, 
        DownLeft,
        Left, 
        UpLeft,
    }
}