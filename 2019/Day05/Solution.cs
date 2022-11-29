using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text.RegularExpressions;
using System.Text;

namespace AdventOfCode.Y2019.Day05;

[ProblemName("Sunny with a Chance of Asteroids")]
class Solution : Solver
{
    public object PartOne(string input)
    {
        var computer = new IntCodePC();
        var program = Utility.ParseNumberListInput(input, ",").ToList();
        var outputs = new List<int>();
        computer.RunProgram(program, 0, outputs);
        return outputs[outputs.Count - 1];
    }

    public object PartTwo(string input)
    {
        return 0;
    }
}