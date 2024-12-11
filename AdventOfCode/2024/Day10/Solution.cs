using AdventOfCode.Core;
using AdventOfCode.Core.Common;
using AdventOfCode.Core.Extensions;
using AdventOfCode.Core.Math;

namespace AdventOfCode._2024.Day10;

[ProblemName("Hoof It")]
public class Solution : SolverBase<int, Grid<TerrainTile>>
{
    protected override int SolvePartOne(Grid<TerrainTile> input) => input.Where(x => x.Value.Height == 0).Sum(x => NumberOfTrails(input, x.Position));

    protected override int SolvePartTwo(Grid<TerrainTile> input)
    {
        throw new NotImplementedException();
    }

    protected override Grid<TerrainTile> Parse(string input) => new(input.GetLines().Select(x => x.ToCharArray().Select(c => new TerrainTile(int.Parse(c.ToString())))));

    private int NumberOfTrails(Grid<TerrainTile> map, Vector2Int trailHead)
    {
        List<Vector2Int> foundTrails = [];
        
        Queue<Vector2Int> positionsToCheck = new([trailHead]);
        while (positionsToCheck.Count != 0)
        {
            var currentNode = map.GetNodeOrDefault(positionsToCheck.Dequeue());
            if (currentNode == null) continue;
            
            if (currentNode.Value.Height == 9 && !foundTrails.Contains(currentNode.Position))
            {
                foundTrails.Add(currentNode.Position);
                continue;
            }

            foreach (var neighbor in map.GetNeighbors(currentNode.Position).Where(neighbor => neighbor.Value.Height == currentNode.Value.Height + 1))
            {
                positionsToCheck.Enqueue(neighbor.Position);
            }
        }
        
        return foundTrails.Count;
    }
}

public record struct TerrainTile(int Height);