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
        var computer = new Day5PC(1, input);
        var outputs = computer.outputs;
        computer.StartProgram();
        return outputs[outputs.Count - 1];
    }

    public object PartTwo(string input)
    {
        var computer = new Day5PC(5, input);
        var outputs = computer.outputs;
        computer.StartProgram();
        return outputs[outputs.Count - 1];
    }
}

class Day5PC : IntCodePC
{
    private int inputValue;

    public Day5PC(int inputValue, string input) : base(input)
    {
        this.inputValue = inputValue;
    }

    public override double GetInputValue()
    {
        return inputValue;
    }
}