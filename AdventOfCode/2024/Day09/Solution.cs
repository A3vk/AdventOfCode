using AdventOfCode.Core;

namespace AdventOfCode._2024.Day09;

using FileSystem = System.Collections.Generic.LinkedList<FileBlock>;
using Node = LinkedListNode<FileBlock>;
public record struct FileBlock(int FileId, int Size);

[ProblemName("Disk Fragmenter")]
public class Solution : SolverBase<long, FileSystem>
{
    protected override long SolvePartOne(FileSystem input) => CalculateChecksum(CompactFileSystem(input, true));

    protected override long SolvePartTwo(FileSystem input) => CalculateChecksum(CompactFileSystem(input, false));

    private FileSystem CompactFileSystem(FileSystem fileSystem, bool isFragmentationEnabled)
    {
        var (start, end) = (fileSystem.First, fileSystem.Last);
        while (start != end)
        {
            if (start!.Value.FileId != -1)
            {
                start = start.Next;
            }
            else if (end!.Value.FileId == -1)
            {
                end = end.Previous;
            }
            else
            {
                RelocateFile(fileSystem, start, end, isFragmentationEnabled);
                end = end.Previous;
            }
        }
        
        return fileSystem;
    }

    private void RelocateFile(FileSystem fileSystem, Node start, Node end, bool isFragmentationEnabled)
    {
        for (var current = start; current != end; current = current.Next!)
        {
            if (current.Value.FileId != -1) continue;
            
            // Size is the same, swap them
            if (current.Value.Size == end.Value.Size)
            {
                (current.Value, end.Value) = (end.Value, current.Value);
                return;
            }
            
            // Empty space is larger than the file size, swap and add empty space after current
            if (current.Value.Size > end.Value.Size)
            {
                var remainingSize = current.Value.Size - end.Value.Size;
                current.Value = end.Value;
                end.Value = end.Value with { FileId = -1 };
                fileSystem.AddAfter(current, new FileBlock(-1, remainingSize));
                return;
            }
            
            // Empty space is smaller than the file size, when fragmentation is enabled, chop the file and add an empty space after the end
            if (isFragmentationEnabled && current.Value.Size < end.Value.Size)
            {
                var remainingSize = end.Value.Size - current.Value.Size;
                current.Value = current.Value with { FileId = end.Value.FileId };
                end.Value = end.Value with { Size = remainingSize };
                fileSystem.AddAfter(end, current.Value with { FileId = -1 });
            }
        }
    }

    private long CalculateChecksum(FileSystem fileSystem)
    {
        int position = 0;
        
        long checksum = 0;
        foreach (var file in fileSystem)
        {
            if (file.FileId == -1)
            {
                position += file.Size;
                continue;
            }

            for (int i = 0; i < file.Size; i++)
            {
                checksum += position * file.FileId;
                position++;
            }
        }
        
        return checksum;
    }

    protected override FileSystem Parse(string input) => new (input.Select((c, i) => new FileBlock(i % 2 == 1 ? -1 : i / 2, c - '0')));
}