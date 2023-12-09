using System.Text.RegularExpressions;
using AdventOfCode.Core;

var runner = new SolutionRunner();
var results = new List<SolverResult>();

if (args is ["all"])
{
    results.AddRange(runner.Run().OrderBy(x => x.SolverType.FullName));
} 
else if (args is ["today"])
{
    DateTime today = DateTime.Now;
    results.Add(runner.Run(today.Year, today.Day));
} 
else if (args.Length >= 1 && Regex.Match(args[0], @"^([0-9]+)$").Success)
{
    var year = int.Parse(Regex.Match(args[0], @"^([0-9]+)$").Groups[0].Value);
    results.AddRange(runner.Run(year).OrderBy(x => x.SolverType.FullName));
}
else if (args.Length >= 1 && Regex.Match(args[0], @"^([0-9]+)/([0-9]+)$").Success)
{
    var match = Regex.Match(args[0], @"^([0-9]+)/([0-9]+)$");
    var year = int.Parse(match.Groups[1].Value);
    var day = int.Parse(match.Groups[2].Value);
    results.Add(runner.Run(year, day, args.Contains("sample")));
}
else
{
    results.Add(runner.Run(2023, 7, false));
}

// DateTime today = DateTime.Now;
// // results.AddRange(runner.Run(today.Year).OrderBy(x => x.SolverType.FullName));
// results.Add(runner.Run(today.Year, today.Day));
//
// var todayResult = runner.Run(today.Year, today.Day);
//
foreach (var result in results)
{
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
//
// Console.WriteLine("Done!");