using AdventOfCode.Core;
using AdventOfCode.Core.Common;
using AdventOfCode.Core.Extensions;
using AdventOfCode.Core.Math;

namespace AdventOfCode._2024.Day10;

[ProblemName("Hoof It")]
public class Solution : SolverBase<int, Grid<TerrainTile>>
{
    protected override int SolvePartOne(Grid<TerrainTile> input) => input.Where(x => x.Value.Height == 0).Select(x => FindTrails(input, x.Position).Count).Sum();
    protected override int SolvePartTwo(Grid<TerrainTile> input) => input.Where(x => x.Value.Height == 0).SelectMany(x => FindTrails(input, x.Position).Values).Sum();

    protected override Grid<TerrainTile> Parse(string input) => new(input.GetLines().Select(x => x.ToCharArray().Select(c => new TerrainTile(int.Parse(c.ToString())))));

    private Dictionary<Vector2Int, int> FindTrails(Grid<TerrainTile> map, Vector2Int trailHead)
    {
        Dictionary<Vector2Int, int> foundTrails = [];
        
        Queue<Vector2Int> positionsToCheck = new([trailHead]);
        while (positionsToCheck.Count != 0)
        {
            var currentNode = map.GetNode(positionsToCheck.Dequeue());
            
            if (currentNode.Value.Height == 9)
            {
                foundTrails.TryAdd(currentNode.Position, 0);
                foundTrails[currentNode.Position]++;
                continue;
            }

            foreach (var neighbor in map.GetNeighbors(currentNode.Position).Where(neighbor => neighbor.Value.Height == currentNode.Value.Height + 1))
            {
                positionsToCheck.Enqueue(neighbor.Position);
            }
        }
        
        return foundTrails;
    }
}

public record struct TerrainTile(int Height);