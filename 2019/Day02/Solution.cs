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
        return 0;
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