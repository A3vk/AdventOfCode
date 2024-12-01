using System.CommandLine;
using AdventOfCode.Core;

namespace AdventOfCode.CLI;

public class RunCommand : Command
{
    private ISolutionRunner _runner;
    
    public RunCommand() : base("run", "Run a solution")
    {
        var sampleOptions = new Option<bool>("--sample", "If this flag is present, sample data will be used");
        
        AddOption(SharedOptions.YearOption);
        AddOption(SharedOptions.DayOption);
        AddOption(sampleOptions);
        
        this.SetHandler(ExecuteCommand, SharedOptions.YearOption, SharedOptions.DayOption, sampleOptions);
        
        _runner = new SolutionRunner();
    }

    private void ExecuteCommand(int year, int day, bool useSample)
    {
        Console.WriteLine($"Running solution for {year}-{day}. Use sample {useSample}");
        var result = _runner.RunDay(year, day, useSample);
        
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine($"---    Year {result.SolverType.Year()} Day {result.SolverType.Day()}: {result.SolverType.GetName()}    ---");

        Console.ForegroundColor = ConsoleColor.Gray;
        foreach (var answer in result.Answers)
        {
         Console.WriteLine(answer);
        }

        Console.ForegroundColor = ConsoleColor.Red;
        foreach (var error in result.Errors)
        {
         Console.WriteLine(error);
        }
        Console.ForegroundColor = ConsoleColor.Gray;
    }
}