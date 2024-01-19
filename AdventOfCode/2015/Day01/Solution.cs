using AdventOfCode.Core;

namespace AdventOfCode._2015.Day01;

[ProblemName("Not Quite Lisp")]
public class Solution : ISolver
{
    public string PartOne(string input)
    {
        return (input.Count(c => c == '(') - input.Count(c => c == ')')).ToString();
    }

    public string PartTwo(string input)
    {
        int floor = 0;
        for (int i = 0; i < input.Length; i++)
        {
            floor += input[i] == '(' ? 1 : -1;
            if (floor < 0) return (i + 1).ToString();
        }

        throw new InvalidOperationException("Santa never enters the basement");
    }
}

