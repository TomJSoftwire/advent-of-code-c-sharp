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
        var max = calPerElf.Aggregate(0, (max, curr) => curr < max ? max : curr);
        return max;
    }

    public object PartTwo(string input)
    {
        var foodPerElf = ParseInput(input);
        var calPerElf = foodPerElf.Select(elf => elf.Aggregate(0, (total, item) => total + item));
        var max = calPerElf.Aggregate(0, (max, curr) => curr < max ? max : curr);
        var second = calPerElf.Where(cal => cal != max).Aggregate(0, (max, curr) => curr < max ? max : curr);
        var third = calPerElf.Where(cal => cal != second && cal != max).Aggregate(0, (max, curr) => curr < max ? max : curr);
        return max + second + third;
    }

    IEnumerable<IEnumerable<int>> ParseInput(string input)
    {
        var r = input.Split("\n\n").Select((elf) => elf.Split('\n').Select(food => int.Parse(food)));
        return r;
    }
}