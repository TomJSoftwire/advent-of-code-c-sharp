using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text.RegularExpressions;
using System.Text;

namespace AdventOfCode.Y2022.Day03;

[ProblemName("Rucksack Reorganization")]
class Solution : Solver
{
    public object PartOne(string input)
    {
        var backpacks = Read(input);
        var priorities = from b in backpacks select GetPriority(FindMatch(b));
        return priorities.Sum();
    }

    public object PartTwo(string input)
    {
        return 0;
    }

    int GetPriority(char letter)
    {
        var adjustedCode = (int)letter - 96;
        return adjustedCode > 0 ? adjustedCode : (adjustedCode + 31 + 27);
    }

    char FindMatch((string, string) backpack)
    {
        var (a, b) = backpack;
        var intersection = a.Intersect(b).ToList();
        return intersection[0];
    }

    IEnumerable<(string, string)> Read(string input)
        =>
            from backback in input.Split('\n')
            let size = backback.Length
            select (backback.Substring(0, size / 2), backback.Substring(size / 2));
}