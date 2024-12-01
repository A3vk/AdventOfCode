using System.Collections.Immutable;
using System.Reflection;
using AdventOfCode.Core;

namespace AdventOfCode;

public class SolutionRunner : ISolutionRunner
{
    private static readonly ImmutableList<Type> SolverTypes = Assembly.GetExecutingAssembly()
        .GetTypes()
        .Where(t => t.IsClass &&
                    typeof(ISolver).IsAssignableFrom(t) &&
                    t.GetCustomAttributes<ProblemNameAttribute>()
                        .Any())
        .ToImmutableList();

    public IEnumerable<SolverResult> Run()
    {
        var solvers = SolverTypes.Select(s => s.Activate());
        
        return ExecuteSolver(solvers);;
    }

    public IEnumerable<SolverResult> RunYear(int year)
    {
        var solvers = SolverTypes.Where(s => s.Year() == year).Select(s => s.Activate());

        return ExecuteSolver(solvers);
    }

    public SolverResult RunDay(int year, int day, bool useSample = false)
    {
        var solver = SolverTypes.FirstOrDefault(s => s.Year() == year && s.Day() == day)?.Activate();
        if (solver == null) throw new InvalidOperationException("Solver not found");

        return ExecuteSolver([solver], useSample).First();
    }

    private IEnumerable<SolverResult> ExecuteSolver(IEnumerable<ISolver> solvers, bool useSample = false)
    {
        var results = new List<SolverResult>();
        
        foreach (var solver in solvers)
        {
            var result = new SolverResult(solver);
            var input = useSample ? solver.GetSampleInput() : solver.GetInput();

            try
            {
                try
                {
                    result.Answers.Add($"Part one result: {solver.PartOne(input)}");
                }
                catch (NotImplementedException)
                {
                    result.Errors.Add("Part one is not implemented yet");
                }

                try
                {
                    result.Answers.Add($"Part two result: {solver.PartTwo(input)}");
                }
                catch (NotImplementedException)
                {
                    result.Errors.Add("Part two is not implemented yet");
                }
            }
            catch (Exception e)
            {
                result.Errors.Add($"Unknown exception occured: {e}");
            }
            results.Add(result);
        }
        
        return results;
    }
}