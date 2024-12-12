using AdventOfCode.Core;
using AdventOfCode.Core.Common;
using AdventOfCode.Core.Extensions;
using AdventOfCode.Core.Math;

namespace AdventOfCode._2024.Day12;

[ProblemName("Garden Groups")]
public class Solution : SolverBase<int, Grid<char>>
{
    private readonly List<(Vector2Int, Vector2Int)> _cornerDirectionPairs = [
        (Vector2Int.Up, Vector2Int.Right), 
        (Vector2Int.Right, Vector2Int.Down), 
        (Vector2Int.Down, Vector2Int.Left), 
        (Vector2Int.Left, Vector2Int.Up)
    ];
    
    protected override int SolvePartOne(Grid<char> input) => Solve(input, CalculateFencePerimeterPrice);
    protected override int SolvePartTwo(Grid<char> input) => Solve(input, CalculateFenceSidePrice);
    protected override Grid<char> Parse(string input) => Grid<char>.CreateCharGrid(input);

    private int Solve(Grid<char> input, Func<Grid<char>, Region, int> priceCalculationFunc) => FindRegions(input).Sum(x => priceCalculationFunc(input, x));

    private List<Region> FindRegions(Grid<char> garden)
    {
        List<Region> regions = [];
        
        foreach (var plot in garden)
        {
            if (regions.Any(x => x.Plots.Contains(plot.Position))) continue;

            var region = new Region(plot.Value, []);
            Queue<Vector2Int> plotsToCheck = new ([plot.Position]);
            while (plotsToCheck.Any())
            {
                var currentPlot = plotsToCheck.Dequeue();
                if (region.Plots.Contains(currentPlot)) continue;
                
                region.Plots.Add(currentPlot);
                
                plotsToCheck.EnqueueRange(garden.GetNeighbors(currentPlot).Where(x => x.Value == region.PlantType).Select(x => x.Position));
            }
            
            regions.Add(region);
        }
        
        return regions;
    }

    private int CalculateFencePerimeterPrice(Grid<char> garden, Region region)
    {
        int area = region.Plots.Count;
        int perimeter = region.Plots.Sum(plot => 4 - garden.GetNeighbors(plot).Count(x => x.Value == region.PlantType));

        return area * perimeter;
    }

    private int CalculateFenceSidePrice(Grid<char> garden, Region region)
    {
        int area = region.Plots.Count;

        // The number of corners is always the same as the number of sides
        int numberOfCorners = 0;
        foreach (var plot in region.Plots)
        {
            
            foreach (var directionPair in _cornerDirectionPairs)
            {
                // Check outer corner
                if (garden.GetValueOrDefault(plot + directionPair.Item1) != region.PlantType && 
                    garden.GetValueOrDefault(plot + directionPair.Item2) != region.PlantType)
                {
                    numberOfCorners++;
                }
                
                // Check inner corner
                if (garden.GetValueOrDefault(plot + directionPair.Item1) == region.PlantType &&
                    garden.GetValueOrDefault(plot + directionPair.Item2) == region.PlantType &&
                    garden.GetValueOrDefault(plot + directionPair.Item1 + directionPair.Item2) != region.PlantType)
                {
                    numberOfCorners++;
                }
            }
        }
        
        return area * numberOfCorners;
    }
}

public record struct Region(char PlantType, List<Vector2Int> Plots);