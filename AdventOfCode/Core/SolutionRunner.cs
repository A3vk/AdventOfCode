using System.Reflection;

namespace AdventOfCode.Core;

public class SolutionRunner
{
    public List<Type> Solvers;
    
    public SolutionRunner()
    {
        Solvers = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.IsClass && typeof(ISolver).IsAssignableFrom(t) && t.GetCustomAttributes(typeof(ProblemNameAttribute)).Any()).ToList();
    }

    public List<SolverResult> Run()
    {
        var results = new List<SolverResult>();
        var solvers = Solvers.Select(s => s.Activate());
        foreach (var solver in solvers)
        {
            var result = new SolverResult(solver);
            var input = solver.GetInput();

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

    public List<SolverResult> Run(int year)
    {
        var results = new List<SolverResult>();
        var solvers = Solvers.Where(s => s.Year() == year).Select(s => s.Activate());
        foreach (var solver in solvers)
        {
            var result = new SolverResult(solver);
            var input = solver.GetInput();

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

    public SolverResult Run(int year, int day, bool useSample = false)
    {
        var solver = Solvers.FirstOrDefault(s => s.Year() == year && s.Day() == day)?.Activate();
        if (solver == null) throw new InvalidOperationException("Solver not found");

        var result = new SolverResult(solver);

        try
        {
            var input = useSample ? solver.GetSampleInput() : solver.GetInput();
            
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

        return result;
    }
}