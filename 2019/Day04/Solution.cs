using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text.RegularExpressions;
using System.Text;

namespace AdventOfCode.Y2019.Day04;

[ProblemName("Secure Container")]
class Solution : Solver
{
    public object PartOne(string input)
    {
        var (min, max) = GetRange(input);
        var count = 0;
        for (var i = min; i < max; i++)
        {
            if (ValidateNumber(i))
            {
                count++;
            }
        }

        return count;
    }

    public object PartTwo(string input)
    {
        return 0;
    }

    bool ValidateNumber(int number)
    {
        var stringNum = number.ToString();
        var repeatedNumRx = new Regex(@"11|22|33|44|55|66|77|88|99|00");
        var orderedNumRx = new Regex(@"^0*1*2*3*4*5*6*7*8*9*$");
        if (repeatedNumRx.IsMatch(stringNum) && orderedNumRx.IsMatch(stringNum))
        {
            return true;
        }

        return false;
    }
    (int, int) GetRange(string input)
    {
        var strs = input.Split('-');
        var nums = (from str in strs select int.Parse(str)).ToList();
        return (nums[0], nums[1]);
    }
}