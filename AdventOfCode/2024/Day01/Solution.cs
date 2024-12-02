using AdventOfCode.Core;
using AdventOfCode.Core.Extensions;

namespace AdventOfCode._2024.Day01;

[ProblemName("Historian Hysteria")]
public class Solution : ISolver
{
    public string PartOne(string input)
    {
        var (leftLocationIds, rightLocationIds) = GetLocationIdsListsFromInput(input);
        leftLocationIds = leftLocationIds.Order().ToList();
        rightLocationIds = rightLocationIds.Order().ToList();

        int totalDistance = leftLocationIds.Select((locationId, index) => Math.Abs(locationId - rightLocationIds[index])).Sum();

        return totalDistance.ToString();
    }

    public string PartTwo(string input)
    {
        var (leftLocationIds, rightLocationIds) = GetLocationIdsListsFromInput(input);

        int totalSimilarityScore = 0;
        foreach (var locationId in leftLocationIds)
        {
            int numberOfAppearances = rightLocationIds.Count(x => x == locationId);
            int similarityScore = locationId * numberOfAppearances;
            totalSimilarityScore += similarityScore;
        }

        return totalSimilarityScore.ToString();
    }

    private (List<int> lefft, List<int> right) GetLocationIdsListsFromInput(string input)
    {
        var lines = input.GetLines();

        List<int> left = [];
        List<int> right = [];
        foreach (var locationIds in lines.Select(line => line.Split("   ").Select(int.Parse).ToArray()))
        {
            left.Add(locationIds[0]);
            right.Add(locationIds[1]);
        }
        return (left, right);
    }
}