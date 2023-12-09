using System.Collections.Immutable;
using AdventOfCode.Core;
using AdventOfCode.Core.Utils;

namespace AdventOfCode._2023.Day07;

[ProblemName("Camel Cards")]
public class Solution : ISolver
{
    private static readonly ImmutableList<char> CardsLookup = ImmutableList.Create('2', '3', '4', '5', '6', '7', '8', '9', 'T', 'J', 'Q', 'K', 'A');
    
    public string PartOne(string input)
    {
        var hands = input.GetLines().Select(ParseHand).ToList();
        var rankedHands = RankHands(hands);
        return rankedHands.Select((hand, index) => hand.Bid * (index + 1)).Sum().ToString();
    }

    public string PartTwo(string input)
    {
        throw new NotImplementedException();
    }

    private Hand ParseHand(string line)
    {
        var parts = line.Split(" ");
        return new Hand(parts[0].ToList(), int.Parse(parts[1]));
    }

    private List<Hand> RankHands(List<Hand> hands)
    {
        List<Hand> fiveOfAKind = new();
        List<Hand> fourOfAKind = new();
        List<Hand> fullHouse = new();
        List<Hand> threeOfAKind = new();
        List<Hand> twoPair = new();
        List<Hand> onePair = new();
        List<Hand> highCard = new();

        foreach (var hand in hands)
        {
            var groupedHand = hand.Cards.GroupBy(c => c).ToList();
            if (groupedHand.Count == 1)
            {
                fiveOfAKind.Add(hand);
            }
            else if (groupedHand.Any(x => x.Count() == 4))
            {
                fourOfAKind.Add(hand);
            }
            else if (groupedHand.Any(x => x.Count() == 3) && groupedHand.Any(x => x.Count() == 2))
            {
                fullHouse.Add(hand);
            }
            else if (groupedHand.Any(x => x.Count() == 3))
            {
                threeOfAKind.Add(hand);
            }
            else if (groupedHand.Count(x => x.Count() == 2) == 2)
            {
                twoPair.Add(hand);
            }
            else if (groupedHand.Any(x => x.Count() == 2))
            {
                onePair.Add(hand);
            }
            else
            {
                highCard.Add(hand);
            }
        }

        fiveOfAKind.Sort();
        fourOfAKind.Sort();
        fullHouse.Sort();
        threeOfAKind.Sort();
        twoPair.Sort();
        onePair.Sort();
        highCard.Sort();

        return highCard.Concat(onePair).Concat(twoPair).Concat(threeOfAKind).Concat(fullHouse).Concat(fourOfAKind).Concat(fiveOfAKind).ToList();
    }

    private record Hand(List<char> Cards, int Bid) : IComparable<Hand>
    {
        public int GetCardValue => Cards.Sum(c => CardsLookup.IndexOf(c));

        public int CompareTo(Hand? other)
        {
            if (other == null) return -1;
            
            for (int i = 0; i < 5; i++)
            {
                
                var currentCard = Cards[i];
                var currentOtherCard = other.Cards[i];

                if (CardsLookup.IndexOf(currentCard) < CardsLookup.IndexOf(currentOtherCard))
                {
                    return -1;
                }

                if (CardsLookup.IndexOf(currentCard) > CardsLookup.IndexOf(currentOtherCard))
                {
                    return 1;
                }
            }

            return 0;
        }
    }
}

