using System.Text.RegularExpressions;
using AdventOfCode.Core;
using AdventOfCode.Core.Extensions;
using AdventOfCode.Core.Math;

namespace AdventOfCode._2024.Day13;

[ProblemName("Claw Contraption")]
public partial class Solution : SolverBase<long, IEnumerable<ClawMachine>>
{
    protected override long SolvePartOne(IEnumerable<ClawMachine> input) => input.Select(x => NumberOfTokensToGetPrize(x)).Sum();
    protected override long SolvePartTwo(IEnumerable<ClawMachine> input) => input.Select(x => NumberOfTokensToGetPrize(x, 10000000000000)).Sum();
    
    protected override IEnumerable<ClawMachine> Parse(string input)
    {
        var clawMachineLines = input.Split("\n\n");
        foreach (var clawMachineLine in clawMachineLines)
        {
            var vectors = GetDigitRegex().Matches(clawMachineLine)
                .Select(x => long.Parse(x.Value))
                .Chunk(2)
                .Select(x => new Vector2Long(x[0], x[1]))
                .ToArray();
            
            yield return new ClawMachine(vectors[0], vectors[1], vectors[2]);
        }
    }

    // Using Carmer's rule for the following formulas
    // ButtonA.X * ButtonAPressed + ButtonB.X * ButtonBPressed = Prize.X
    // ButtonA.Y * ButtonAPressed + ButtonB.Y * ButtonBPressed = Prize.Y
    private long NumberOfTokensToGetPrize(ClawMachine clawMachine, long offset = 0)
    {
        if (offset != 0)
        {
            clawMachine.Prize = clawMachine.Prize + offset;
        }
        
        var determinant = Determinant(clawMachine.ButtonA, clawMachine.ButtonB);
        var buttonAPressed = Determinant(clawMachine.Prize, clawMachine.ButtonB) / determinant;
        var buttonBPressed = Determinant(clawMachine.ButtonA, clawMachine.Prize) / determinant;

        if (buttonAPressed >= 0 && buttonBPressed >= 0 && 
            clawMachine.ButtonA.X * buttonAPressed + clawMachine.ButtonB.X * buttonBPressed == clawMachine.Prize.X && 
            clawMachine.ButtonA.Y * buttonAPressed + clawMachine.ButtonB.Y * buttonBPressed == clawMachine.Prize.Y)
        {
            return buttonAPressed * 3 + buttonBPressed;
        }

        return 0;
    }
    
    private long Determinant(Vector2Long a, Vector2Long b) => a.X * b.Y - a.Y * b.X;
    
    [GeneratedRegex(@"\d+")]
    private static partial Regex GetDigitRegex();
}

public record struct ClawMachine(Vector2Long ButtonA, Vector2Long ButtonB, Vector2Long Prize);