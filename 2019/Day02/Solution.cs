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
        var program = Utility.ParseNumberListInput(input, ",").ToList();
        program[1] = 12;
        program[2] = 2;
        var result = RunProgram(program);
        return result[0];
    }

    public object PartTwo(string input)
    {
        var program = Utility.ParseNumberListInput(input, ",").ToList();
        for (int i = 0; i < 100; i++)
        {
            for (int j = 0; j < 100; j++)
            {
                // Console.WriteLine();
                var newList = program.ToList();
                foreach (var VARIABLE in newList)
                {
                    // Console.Write(VARIABLE + ",");
                }

                program[1] = i;
                program[2] = j;
                if (RunProgram(newList)[0] == 19690720)
                {
                    return i * 100 + j;
                }
            }
        }

        return -1;
    }

    List<int> RunProgram(List<int> list, int index = 0)
        => (list[index]) switch
        {
            1 => RunProgram(PerformAdd(list, index + 1), index + 4),
            2 => RunProgram(PerformMultiply(list, index + 1), index + 4),
            99 => list,
            _ => throw new Exception()
        };


    List<int> PerformAdd(List<int> list, int startIndex)
    {
        var (a, b, resultIndex) = GetValuesAndTarget(list, startIndex);
        list[resultIndex] = a + b;
        return list;
    }

    List<int> PerformMultiply(List<int> list, int startIndex)
    {
        var (a, b, resultIndex) = GetValuesAndTarget(list, startIndex);
        list[resultIndex] = a * b;
        return list;
    }

    ValueIndeces GetValuesAndTarget(List<int> list, int startIndex) =>
        new ValueIndeces(list[list[startIndex]], list[list[startIndex + 1]], list[startIndex + 2]);
}

record ValueIndeces(int a, int b, int result);