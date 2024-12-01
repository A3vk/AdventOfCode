using System.Collections.Immutable;
using AdventOfCode.Core;
using AdventOfCode.Core.Extensions;

namespace AdventOfCode._2023.Day07;

[ProblemName("Camel Cards")]
public class Solution : ISolver
{
    private static bool _useJokers = false;
    private static ImmutableList<char> _cardsLookup = ImmutableList<char>.Empty;
    
    public string PartOne(string input)
    {
        _cardsLookup = ImmutableList.Create('2', '3', '4', '5', '6', '7', '8', '9', 'T', 'J', 'Q', 'K', 'A');
        
        var hands = input.GetLines().Select(ParseHand).ToList();
        hands.Sort();
        return hands.Select((hand, index) => hand.Bid * (index + 1)).Sum().ToString();
    }

    public string PartTwo(string input)
    {
        _cardsLookup = ImmutableList.Create('J', '2', '3', '4', '5', '6', '7', '8', '9', 'T', 'Q', 'K', 'A');
        _useJokers = true;
        
        var hands = input.GetLines().Select(ParseHand).ToList();
        hands.Sort();
        return hands.Select((hand, index) => hand.Bid * (index + 1)).Sum().ToString();
    }

    private Hand ParseHand(string line)
    {
        var parts = line.Split(" ");
        return new Hand(parts[0].ToList(), int.Parse(parts[1]));
    }

    private record Hand(List<char> Cards, int Bid) : IComparable<Hand>
    {
        private int GetValue()
        {
            return !_useJokers ? GetCardsValue(Cards) : Cards.Select(card => GetCardsValue(Cards.Select(x => x == 'J' ? card : x).ToList())).Max();
        }

        private static int GetCardsValue(List<char> cards) => cards.Select(currentCard => cards.Count(card => currentCard == card)).Sum();

        public int CompareTo(Hand? other)
        {
            if (other == null) return -1;

            if (GetValue() < other.GetValue()) return -1;
            if (GetValue() > other.GetValue()) return 1;
            
            for (int i = 0; i < 5; i++)
            {
                if (_cardsLookup.IndexOf(Cards[i]) < _cardsLookup.IndexOf(other.Cards[i])) return -1;
                if (_cardsLookup.IndexOf(Cards[i]) > _cardsLookup.IndexOf(other.Cards[i])) return 1;
            }

            return 0;
        }
    }
}
