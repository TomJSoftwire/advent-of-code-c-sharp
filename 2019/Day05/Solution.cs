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
        var computer = new Day5PC(1);
        var program = Utility.ParseNumberListInput(input, ",").ToList();
        var outputs = new List<int>();
        computer.RunProgram(program, 0, outputs);
        return outputs[outputs.Count - 1];
    }

    public object PartTwo(string input)
    {
        var computer = new Day5PC(5);
        var program = Utility.ParseNumberListInput(input, ",").ToList();
        var outputs = new List<int>();
        computer.RunProgram(program, 0, outputs);
        return outputs[outputs.Count - 1];
    }
}

class Day5PC : IntCodePC
{
    private int inputValue;
    public Day5PC(int inputValue)
    {
        this.inputValue = inputValue;
    }
    public override List<int> PerformInput(List<int> list, int index, List<int> parameters)
    {
        var newList = list.ToList();
        var writeLocation = parameters[0];
        newList[writeLocation] = inputValue;
        return newList;
    }
}