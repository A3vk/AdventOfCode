using AdventOfCode.Core;
using AdventOfCode.Core.Extensions;
using AdventOfCode.Core.Math;

namespace AdventOfCode._2024.Day05;

[ProblemName("Print Queue")]
public class Solution : ISolver
{
    public string PartOne(string input)
    {
        var inputs = input.SplitOnBlankLines();
        var pageOrderingRules = ParsePageOrderingRules(inputs[0]);
        var updates = ParseUpdates(inputs[1]);

        int result = 0;
        foreach (var update in updates)
        {
            if (IsValid(update, pageOrderingRules))
            {
                result += update[update.Count / 2];
            }
        }
        
        return result.ToString();
    }

    public string PartTwo(string input)
    {
        var inputs = input.SplitOnBlankLines();
        var pageOrderingRules = ParsePageOrderingRules(inputs[0]);
        var updates = ParseUpdates(inputs[1]);

        int result = 0;
        foreach (var update in updates)
        {
            if (!IsValid(update, pageOrderingRules))
            {
                OrderUpdate(update, pageOrderingRules);
                result += update[update.Count / 2];
            }
        }
        
        return result.ToString();
    }

    private bool IsValid(List<int> update, List<Vector2Int> printOrderingRules)
    {
        List<int> previousPages = [];
        foreach (var page in update)
        {
            var applicableOrderingRules = printOrderingRules.FindAll(rule => rule.X == page);
            if (applicableOrderingRules.Any(rule => previousPages.Contains(rule.Y))) return false;
            
            previousPages.Add(page);
        }

        return true;
    }

    private void OrderUpdate(List<int> update, List<Vector2Int> printOrderingRules)
    {
        for (int i = 0; i < update.Count; i++)
        {
            var applicableOrderingRules = printOrderingRules.FindAll(rule => rule.X == update[i]);
            foreach (var rule in applicableOrderingRules)
            {
                var index = update.IndexOf(rule.Y);
                if (index < 0 || index >= i) continue;
                
                update.RemoveAt(index);
                update.Insert(i, rule.Y);

                i = index - 1;
            }
        }
    }

    private List<Vector2Int> ParsePageOrderingRules(string input)
    {
        var result = input.GetLines().Select(x => x.Split('|').Select(int.Parse).ToArray()).ToList();
        return result.Select(x => new Vector2Int(x[0], x[1])).ToList();
    }

    private List<List<int>> ParseUpdates(string input) =>
        input.GetLines().Select(x => x.Split(',').Select(int.Parse).ToList()).ToList();
}