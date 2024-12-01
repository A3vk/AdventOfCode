using System.CommandLine;

namespace AdventOfCode.CLI;

public static class SharedOptions
{
    public static readonly Option<int> YearOption = new(name: "--year", description: "AoC year", getDefaultValue: () => DateTime.Now.Year);
    public static readonly Option<int> DayOption = new(name: "--day", description: "AoC day", getDefaultValue: () => DateTime.Now.Day);       
}