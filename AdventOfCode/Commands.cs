using AdventOfCode.Core;

namespace AdventOfCode;

public abstract class Command
{
    protected abstract List<string> Regexes { get; }

    public abstract List<SolverResult> Execute(string[] args);
    public abstract bool CanExecute(string[] args);
}

