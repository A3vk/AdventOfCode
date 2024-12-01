using System.CommandLine;
using AdventOfCode.CLI;

var rootCommand = new RootCommand("Advent of Code (AoC) CLI tool.");
rootCommand.Add(new RunCommand());
rootCommand.Add(new DownloadCommand());

return await rootCommand.InvokeAsync(args);