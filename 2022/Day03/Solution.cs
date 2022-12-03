using System;
using System.Collections.Generic;
using System.Collections.Immutable;
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

    public object PartTwo(string input)
    {
        var bags = input.Split('\n').ToList();
        var total = 0;
        for (var i = 0; i < bags.Count; i += 3)
        {
            var group = bags.GetRange(i, 3);
            total += GetPriority(GroupMatches(group));
        }

        return total;
        
    }

    int GetPriority(char letter)
    {
        var adjustedCode = (int)letter - 96;
        return adjustedCode > 0 ? adjustedCode : (adjustedCode + 31 + 27);
    }

    char GroupMatches(IEnumerable<IEnumerable<char>> group)
        => group.Aggregate(group.First(), (matches, bag) => matches.Intersect(bag)).ToList()[0];


    char FindMatch((string, string) backpack)
    {
        var (a, b) = backpack;
        var intersection = a.Intersect(b).ToList();
        return intersection[0];
    }

    IEnumerable<(string, string)> Read1(string input)
        =>
            from backback in input.Split('\n')
            let size = backback.Length
            select (backback.Substring(0, size / 2), backback.Substring(size / 2));
}