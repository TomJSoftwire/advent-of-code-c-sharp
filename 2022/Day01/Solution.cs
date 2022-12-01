using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text.RegularExpressions;
using System.Text;

namespace AdventOfCode.Y2022.Day01;

[ProblemName("Calorie Counting")]
class Solution : Solver
{
    public object PartOne(string input)
    {
        var foodPerElf = ParseInput(input);
        var calPerElf = foodPerElf.Select(elf => elf.Aggregate(0, (total, item) => total + item));
        return calPerElf.OrderDescending().Take(1).Sum();
    }

    public object PartTwo(string input)
    {
        var foodPerElf = ParseInput(input);
        var calPerElf = foodPerElf.Select(elf => elf.Aggregate(0, (total, item) => total + item));
        return calPerElf.OrderDescending().Take(3).Sum();
    }

    IEnumerable<IEnumerable<int>> ParseInput(string input) =>
        input.Split("\n\n").Select((elf) => elf.Split('\n').Select(food => int.Parse(food)));
}