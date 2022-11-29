using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text.RegularExpressions;
using System.Text;

namespace AdventOfCode.Y2019.Day02;

[ProblemName("1202 Program Alarm")]
class Solution : Solver
{
    public object PartOne(string input)
    {
        var computer = new IntCodePC();
        var program = Utility.ParseNumberListInput(input, ",").ToList();
        program[1] = 12;
        program[2] = 2;
        var result = computer.RunProgram(program, 0);
        return result[0];
    }

    public object PartTwo(string input)
    {
        var computer = new IntCodePC();
        var program = Utility.ParseNumberListInput(input, ",").ToList();
        for (int i = 0; i < 100; i++)
        {
            for (int j = 0; j < 100; j++)
            {
                // Console.WriteLine($"i: {i}, j: {j}");
                var newList = program.ToList();
                foreach (var VARIABLE in newList)
                {
                    // Console.Write(VARIABLE + ",");
                }

                newList[1] = i;
                newList[2] = j;
                if (computer.RunProgram(newList, 0)[0] == 19690720)
                {
                    return i * 100 + j;
                }
                // Console.WriteLine(computer.RunProgram(newList)[0]);
            }
        }

        return -1;
    }
}