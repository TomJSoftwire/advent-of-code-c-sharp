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
        return FindBufferEnd(input);
    }

    public object PartTwo(string input)
    {
        return 0;
    }

    int FindBufferEnd(string input)
    {
        var i = 0;
        while (i < input.Length)
        {
            var window = input.Substring(i, 4);
            var uniqueChars = window.Intersect(window).ToList();
            if (uniqueChars.Count() == 4)
            {
                return i + 4;
            }

            i++;
        }

        return -1;
    }
}