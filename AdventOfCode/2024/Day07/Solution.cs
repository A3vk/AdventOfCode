using AdventOfCode.Core;
using AdventOfCode.Core.Extensions;

namespace AdventOfCode._2024.Day07;

[ProblemName("Bridge Repair")]
public class Solution : SolverBase<long, IEnumerable<Equation>>
{
    protected override long SolvePartOne(IEnumerable<Equation> input) => Solve(input, [Operator.Add, Operator.Multiply]);

    protected override  long SolvePartTwo(IEnumerable<Equation> input) => Solve(input, [Operator.Add, Operator.Multiply, Operator.Concatenation]);
    
    protected override IEnumerable<Equation> Parse(string input)
    {
        return input.GetLines().Select(x => new Equation(x)).ToList();
    }

    private long Solve(IEnumerable<Equation> input, List<Operator> operators) => 
        input.Where(equation => IsValid(equation.Result, equation.Values[0], equation.Values[1..], operators)).Sum(equation => equation.Result);

    private bool IsValid(long target, long accumulated, List<long> values, List<Operator> operators)
    {
        if (accumulated > target) return false;
        if (values.Count == 0) return target == accumulated;

        return operators.Any(x => IsValid(target, Calculate(accumulated, values[0], x), values[1..], operators));
    }

    private long Calculate(long a, long b, Operator op) => op switch
    {
        Operator.Add => a + b,
        Operator.Multiply => a * b,
        Operator.Concatenation => long.Parse($"{a}{b}"),
        _ => throw new ArgumentException($"Unknown operator {op}")
    };
}



public class Equation
{
    public long Result { get; }
    public List<long> Values { get; }

    public Equation(string input)
    {
        var resultAndValues = input.Split(": ");
        Result = long.Parse(resultAndValues[0]);
        Values = resultAndValues[1].Split(" ").Select(long.Parse).ToList();
    }
}

public enum Operator
{
    Add,
    Multiply,
    Concatenation
}