using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text.RegularExpressions;
using System.Text;

namespace AdventOfCode.Y2022.Day06;

[ProblemName("Tuning Trouble")]
class Solution : Solver
{
    public object PartOne(string input)
    {
        return FindBufferEnd(input, 4);
    }

    public object PartTwo(string input)
    {
        return FindBufferEnd(input, 14);
    }

    int FindBufferEnd(string input, int uniqueCount)
    {
        var i = 0;
        while (i < input.Length)
        {
            var window = input.Substring(i, uniqueCount);
            var uniqueChars = window.Intersect(window).ToList();
            if (uniqueChars.Count() == uniqueCount)
            {
                return i + uniqueCount;
            }

            i++;
        }

        return -1;
    }
}