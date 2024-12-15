using AdventOfCode.Core.Extensions;

namespace AdventOfCode.Core
{
    public interface ISolver
    {
        string PartOne(string input);
        string PartTwo(string input);
    }

    public abstract class SolverBase<TResult, TParseResult> : ISolver
    {
        public string PartOne(string input) => SolvePartOne(Parse(input.NormalizeBreaks()))?.ToString() ?? throw new NotImplementedException();

        public string PartTwo(string input) => SolvePartTwo(Parse(input.NormalizeBreaks()))?.ToString() ?? throw new NotImplementedException();
        
        protected abstract TResult SolvePartOne(TParseResult input);
        protected abstract TResult SolvePartTwo(TParseResult input);

        protected abstract TParseResult Parse(string input);
    }
}
