namespace AdventOfCode.Core;

public class SolverResult(ISolver solver, List<string>? answers = null, List<string>? errors = null)
{
    public Type SolverType { get; } = solver.GetType();
    public List<string> Answers { get; } = answers ?? new List<string>();
    public List<string> Errors { get; } = errors ?? new List<string>();
}