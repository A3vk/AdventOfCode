using AdventOfCode.Core;

using FileSystem = System.Collections.Generic.List<int>;

namespace AdventOfCode._2024.Day09;

[ProblemName("Disk Fragmenter")]
public class Solution : SolverBase<long, FileSystem>
{
    protected override long SolvePartOne(FileSystem input) => CalculateChecksum(CompactFileSystem(input));

    protected override long SolvePartTwo(FileSystem input)
    {
        throw new NotImplementedException();
    }

    private FileSystem CompactFileSystem(FileSystem fileSystem)
    {
        var (i, j) = (0, fileSystem.Count - 1);
        while (i < j)
        {
            if (fileSystem[i] != -1)
            {
                i++;
            }
            else if (fileSystem[j] == -1)
            {
                j--;
            }
            else
            {
                fileSystem[i] = fileSystem[j];
                fileSystem[j] = -1;
                i++;
                j--;
            }
        }
        
        return fileSystem;
    }

    private long CalculateChecksum(FileSystem fileSystem)
    {
        long checksum = 0;
        for (int i = 0; i < fileSystem.Count; i++)
        {
            if (fileSystem[i] == -1) continue;
            
            checksum += i * fileSystem[i];
        }
        
        return checksum;
    }

    protected override FileSystem Parse(string input) => input.SelectMany((c, i) => Enumerable.Repeat(i % 2 == 1 ? -1 : i / 2, c - '0')).ToList();
}