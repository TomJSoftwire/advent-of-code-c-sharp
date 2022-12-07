using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text.RegularExpressions;
using System.Text;

namespace AdventOfCode.Y2022.Day07;

[ProblemName("No Space Left On Device")]
class Solution : Solver
{
    public object PartOne(string input)
    {
        var storageRoot = new CommunicatorStorage(input).Root;
        var smallDirs = GetDirectoriesSmallerThan(100000, storageRoot);
        return smallDirs.Sum(x => x.GetSize());
    }

    public object PartTwo(string input)
    {
        var storageRoot = new CommunicatorStorage(input).Root;
        var spaceRemaining = 70000000 - storageRoot.GetSize();
        var deletionRequired = 30000000 - spaceRemaining;
        var deletionCandidates = GetDirectoriesLargerThan(deletionRequired, storageRoot);
        return deletionCandidates.Min(x => x.GetSize());
    }

    public List<Directory> GetDirectoriesSmallerThan(int size, Directory directory, List<Directory> resultSizes = null)
    {
        var result = resultSizes == null ? new List<Directory>() : resultSizes;
        var dirSize = directory.GetSize();
        if (dirSize <= size)
        {
            result.Add(directory);
        }

        foreach (var child in directory.children)
        {
            GetDirectoriesSmallerThan(size, child, result);
        }

        return result;
    }

    public List<Directory> GetDirectoriesLargerThan(int size, Directory directory, List<Directory> resultSizes = null)
    {
        var result = resultSizes == null ? new List<Directory>() : resultSizes;
        var dirSize = directory.GetSize();
        if (dirSize >= size)
        {
            result.Add(directory);
        }

        foreach (var child in directory.children)
        {
            GetDirectoriesLargerThan(size, child, result);
        }

        return result;
    }

    
}