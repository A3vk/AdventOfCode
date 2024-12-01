using System.Text.RegularExpressions;
using AdventOfCode.Core;
using AdventOfCode.Core.Extensions;

namespace AdventOfCode._2023.Day04;

[ProblemName("Scratchcards")]
public class Solution : ISolver
{
    public string PartOne(string input)
    {
        var cards = input.GetLines().Select(ParseCard).ToList();
        return cards.Where(card => card.NumberOfMatches() > 0).Sum(card => Math.Pow(2, card.NumberOfMatches() - 1)).ToString();
    }

    public string PartTwo(string input)
    {
        var cards = input.GetLines().Select(ParseCard).ToList();
        var numberOfCards = Enumerable.Repeat(1, cards.Count).ToList();
        for (int i = 0; i < cards.Count; i++)
        {
            for (int j = 0; j < cards[i].NumberOfMatches(); j++)
            {
                numberOfCards[i + j + 1] += numberOfCards[i];
            }
        }

        return numberOfCards.Sum().ToString();
    }

    private Card ParseCard(string value)
    {
        var parts = value.Split(':', '|');
        var winningNumbers = Regex.Matches(parts[1], @"\d+").Select(match => int.Parse(match.Value)).ToList();
        var ownNumbers = Regex.Matches(parts[2], @"\d+").Select(match => int.Parse(match.Value)).ToList();
        return new Card(winningNumbers, ownNumbers);
    }
}

public record Card(List<int> WinningNumbers, List<int> OwnNumbers)
{
    public int NumberOfMatches()
    {
        return WinningNumbers.Intersect(OwnNumbers).Count();
    }
}