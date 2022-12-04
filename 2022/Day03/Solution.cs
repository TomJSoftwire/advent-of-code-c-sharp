using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Data.Common;
using System.Linq;
using System.Text.RegularExpressions;
using System.Text;
using Microsoft.VisualBasic;

namespace AdventOfCode.Y2022.Day03;

[ProblemName("Rucksack Reorganization")]
class Solution : Solver
{
    public object PartOne(string input)
    {
        var backpacks = Read1(input);
        var priorities = from b in backpacks select GetPriority(FindMatch(b));
        return priorities.Sum();
    }

    public object PartTwo(string input) => (
        from g in input.Split('\n').Chunk(3)
        select GetPriority(FindMatch(g))
    ).Sum();

    int GetPriority(char letter)
    {
        var adjustedCode = (int)letter - 96;
        return adjustedCode > 0 ? adjustedCode : (adjustedCode + 31 + 27);
    }

    char FindMatch(IEnumerable<IEnumerable<char>> group)
        => group.Aggregate(group.First(), (matches, bag) => matches.Intersect(bag)).ToList()[0];


    IEnumerable<List<string>> Read1(string input)
        =>
            from backback in input.Split('\n')
            let size = backback.Length
            select new List<string>() { backback.Substring(0, size / 2), backback.Substring(size / 2) };
}