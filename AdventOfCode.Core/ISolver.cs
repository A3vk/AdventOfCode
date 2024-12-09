namespace AdventOfCode.Core
{
    public interface ISolver
    {
        string PartOne(string input);
        string PartTwo(string input);
    }

    public abstract class SolverBase<TResult, TParseResult> : ISolver
    {
        public string PartOne(string input) => SolvePartOne(input)?.ToString() ?? throw new NotImplementedException();

        public string PartTwo(string input) => SolvePartTwo(input)?.ToString() ?? throw new NotImplementedException();
        
        protected abstract TResult SolvePartOne(string input);
        protected abstract TResult SolvePartTwo(string input);

        protected abstract TParseResult Parse(string input);
    }
}
