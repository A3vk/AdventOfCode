using AdventOfCode.Core;
using AdventOfCode.Core.Extensions;
using Report = System.Collections.Generic.List<int>;

namespace AdventOfCode._2024.Day02;

[ProblemName("Red-Nosed Reports")]
public class Solution : ISolver
{
    public string PartOne(string input)
    {
        var reports = GetReports(input);
        return reports.Count(IsSafe).ToString();
    }

    public string PartTwo(string input)
    {
        var reports = GetReports(input);
        return reports.Count(IsSafeWithDampener).ToString();
    }
    
    private static List<Report> GetReports(string input) => input.GetLines().Select(x => x.ToIntList(" ")).ToList();
    
    private static bool IsSafeWithDampener(Report report)
    {
        if (IsSafe(report)) return true;

        for (int i = 0; i < report.Count; i++)
        {
            Report reportWithDampener = [..report];
            reportWithDampener.RemoveAt(i);
            if (IsSafe(reportWithDampener)) return true;
        }
        
        return false;
    }

    private static bool IsSafe(Report report)
    {
        SortOrder order = (SortOrder)report[0].CompareTo(report[1]);
        
        for (int i = 1; i < report.Count; i++)
        {
            var data1 = report[i - 1];
            var data2 = report[i];

            SortOrder currentOrder = (SortOrder)data1.CompareTo(data2);
            if (order != currentOrder || order == SortOrder.None)
            {
                return false;
            }
            
            var diff = Math.Abs(data1 - data2);
            if (diff > 3)
            {
                return false;
            }
        }
        
        return true;
    }

    private enum SortOrder { Ascending = -1, None = 0, Descending = 1 }
}