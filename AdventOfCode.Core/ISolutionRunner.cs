namespace AdventOfCode.Core;

public interface ISolutionRunner
{
    public IEnumerable<SolverResult> Run();
    public IEnumerable<SolverResult> RunYear(int year);
    public SolverResult RunDay(int year, int day, bool useSample = false);
}