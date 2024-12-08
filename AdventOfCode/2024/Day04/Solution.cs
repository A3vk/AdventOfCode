using AdventOfCode.Core;
using AdventOfCode.Core.Common;
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
        int count = 0;
        
        var wordGrid = new WordGrid(input);
        var positions = wordGrid.GetPositionsOfLetter('A');
        
        foreach (var position in positions)
        {
            var bottomLeftPosition = position - Vector2Int.One;
            var subWordGrid = wordGrid.CreateSubWordGrid(bottomLeftPosition, new Vector2Int(3, 3));
            if (subWordGrid is null) 
                continue;
            
            if (subWordGrid.CountOccurrencesInGrid("MAS", true) >= 2)
            {
                count++;
            }
        }
        
        return count.ToString();
    }

    private class WordGrid
    {
        public readonly Grid<char> _grid;
        
        public WordGrid(string input)
        {
            _grid = Grid<char>.CreateCharGrid(input);
        }

        private WordGrid(Grid<char> grid) => _grid = grid;
        
        private readonly Vector2Int[] _directions = [Vector2Int.Down, Vector2Int.Left, Vector2Int.Right, Vector2Int.Up, Vector2Int.DownLeft, Vector2Int.DownRight, Vector2Int.UpLeft, Vector2Int.UpRight];

        public int CountOccurrencesInGrid(string word, bool useOnlyDiagonals = false)
        {
            int result = 0;

            char firstLetter = word[0];
            string neededRemainingWord = word[1..];

            foreach (var node in _grid)
            {
                if (node.Value != firstLetter) continue;
                
                foreach (var direction in _directions)
                {
                    if (useOnlyDiagonals && (direction.X == 0 || direction.Y == 0)) continue;
                    var remainder = GetLettersInDirection(direction, node.Position, neededRemainingWord.Length);
                    if (remainder == neededRemainingWord) { result++; }
                }
            }

            return result;
        }

        public IEnumerable<Vector2Int> GetPositionsOfLetter(char letter) => _grid.Where(x => x.Value == letter).Select(x => x.Position);

        public WordGrid? CreateSubWordGrid(Vector2Int bottomLeftPosition, Vector2Int size)
        {
            var subGrid = _grid.CreateSubGrid(bottomLeftPosition, size);
            return subGrid is null ? null : new WordGrid(subGrid);
        }

        private string GetLettersInDirection(Vector2Int direction, Vector2Int position, int length)
        {
            char[] letters = new char[length];
            
            Vector2Int currentPosition = position + direction;
            for (int i = 0; i < length; i++)
            {
                if (_grid.IsOutOfBounds(currentPosition)) break;
                
                letters[i] = _grid.GetValueOrDefault(currentPosition);
                
                currentPosition += direction;
            }
            
            return new string(letters);
        }
    }
}