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
            if (ValidateNumber(i, new Regex(@"(\d)\1+")))
            {
                count++;
            }
        }

        return count;
    }

    public object PartTwo(string input)
    {
        var (min, max) = GetRange(input);
        var count = 0;
        for (var i = min; i < max; i++)
        {
            if (ValidateNumber(i, BuildExactlyTwoRx()))
            {
                count++;
            }
        }

        return count;
    }

    Regex BuildExactlyTwoRx()
    {
        var rxString = "";
        for (var i = 0; i < 10; i++)
        {
            rxString += $"(?<!{i}){i}{i}(?!{i})|";
        }

        return new Regex(rxString.Substring(0, rxString.Length - 1));
    }

    bool ValidateNumber(int number, Regex repeatedNumRx)
    {
        var stringNum = number.ToString();
        var orderedNumRx = new Regex(@"^0*1*2*3*4*5*6*7*8*9*$");
        return repeatedNumRx.IsMatch(stringNum) && orderedNumRx.IsMatch(stringNum);
    }

    (int, int) GetRange(string input)
    {
        var strs = input.Split('-');
        var nums = (from str in strs select int.Parse(str)).ToList();
        return (nums[0], nums[1]);
    }
}