using System.CommandLine;
using System.Reflection;
using System.Text;
using HtmlAgilityPack;

namespace AdventOfCode.CLI;

public class DownloadCommand : Command
{
    public DownloadCommand() : base("download", "Downloads the information information form AoC and creates a new day (or adds a new part if the day is already created).")
    {
        var outputOption = new Option<string>("--output", "The output directory.");
        outputOption.AddAlias("-o");
        var sessionCookieOption = new Option<string>("--session-cookie", "The session cookie from the AoC website.");
        
        AddOption(SharedOptions.YearOption);
        AddOption(SharedOptions.DayOption);
        AddOption(outputOption);
        AddOption(sessionCookieOption);
        
        this.SetHandler(ExecuteCommand, SharedOptions.YearOption, SharedOptions.DayOption, outputOption, sessionCookieOption);
    }
    
    private async Task ExecuteCommand(int year, int day, string output, string? sessionCookie = null)
    {
        if (string.IsNullOrWhiteSpace(sessionCookie))
        {
            LogError("Session cookie was not specified.");
            return;
        }

        if (string.IsNullOrWhiteSpace(output))
        {
            LogError("Output was not specified.");
            return;
        }
        
        Console.WriteLine($"Downloading files for {year}-{day}");
        
        HttpClient httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Add("Cookie", $"session={sessionCookie}");

        var descriptionResponse = await httpClient.GetAsync($"https://adventofcode.com/{year}/day/{day}");
        if (!descriptionResponse.IsSuccessStatusCode)
        {
            LogError("Failed to get description response.");
            return;
        }
        
        var descriptionContent = await descriptionResponse.Content.ReadAsStringAsync();
        HtmlDocument descriptionDocument = new HtmlDocument();
        descriptionDocument.LoadHtml(descriptionContent);
        
        var description = GetDescription(descriptionDocument);
        if (string.IsNullOrEmpty(description))
        {
            LogError("Failed to get description.");
            return;
        }
        
        var title = GetTitle(descriptionDocument);

        var inputResponse = await httpClient.GetAsync($"https://adventofcode.com/{year}/day/{day}/input");
        if (!inputResponse.IsSuccessStatusCode)
        {
            LogError("Failed to get input response.");
            return;
        }
        string inputContent = await inputResponse.Content.ReadAsStringAsync();
        
        CreateDay(output, year, day, title, description, inputContent);
    }

    private void LogError(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Error.WriteLine(message);
        Console.ResetColor();
    }

    private string GetTitle(HtmlDocument document)
    {
        var titleElement = document.DocumentNode.SelectSingleNode("//h2");
        if (titleElement is null) { return string.Empty; }
        var title = titleElement.InnerText.Split(':')[1].Replace("---", "").Trim();
        return title;
    }
    
    private string GetDescription(HtmlDocument document)
    {
        var dayDescriptions = document.DocumentNode.SelectNodes("//*[@class=\"day-desc\"]");
        if (dayDescriptions is null || dayDescriptions.Count == 0) { return string.Empty; }

        StringBuilder markdownContentBuilder = new StringBuilder();
        var markdownConverter = new ReverseMarkdown.Converter();
        foreach (var dayDescription in dayDescriptions)
        {
            markdownContentBuilder.AppendLine(markdownConverter.Convert(dayDescription.InnerHtml));
        }

        return markdownContentBuilder.ToString();
    }

    private void CreateDay(string directory, int year, int day, string title, string readme, string input)
    {
        var yearPath = Path.Combine(directory, year.ToString());
        var dayPath = Path.Combine(directory, year.ToString(), $"Day{day:D2}");

        if (!Path.Exists(yearPath))
        {
            Directory.CreateDirectory(yearPath);
        }

        if (!Path.Exists(dayPath))
        {
            Directory.CreateDirectory(dayPath);
        }
        var solverAssembly = Assembly.GetExecutingAssembly() ?? throw new InvalidOperationException("Assembly cannot be loaded");
        var assemblyDir = Path.GetDirectoryName(solverAssembly.Location) ?? throw new InvalidOperationException("Assembly location not found");
        DirectoryInfo emptyDayDirectory = new DirectoryInfo(Path.Combine(assemblyDir, "EmptyDay"));
        foreach (var file in emptyDayDirectory.GetFiles())
        {
            try
            {
                file.CopyTo(Path.Combine(dayPath, file.Name), file.Name == "README.md");
            }
            catch
            {
                continue;
            }

            if (file.Name == "Solution.cs")
            {
                string solutionContent = File.ReadAllText(Path.Combine(dayPath, "Solution.cs"));
                solutionContent = solutionContent.Replace("<YEAR>", year.ToString());
                solutionContent = solutionContent.Replace("<DAY>", day.ToString("D2"));
                solutionContent = solutionContent.Replace("<PROBLEM_NAME>", title);
                File.WriteAllText(Path.Combine(dayPath, "Solution.cs"), solutionContent);
            }
            else if (file.Name == "README.md")
            {
                File.WriteAllText(Path.Combine(dayPath, "README.md"), readme);
            }
            else if (file.Name == "input.txt")
            {
                File.WriteAllText(Path.Combine(dayPath, "input.txt"), input.Trim());
            }
        }
    }
}