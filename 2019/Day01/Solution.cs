using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text.RegularExpressions;
using System.Text;

namespace AdventOfCode.Y2019.Day01;

[ProblemName("The Tyranny of the Rocket Equation")]
class Solution : Solver
{
    public object PartOne(string input)
        => (from module in Utility.ParseNumberListInput(input)
                select GetFuelReq(module))
            .Sum();


    public object PartTwo(string input)
        => (from module in Utility.ParseNumberListInput(input)
                select GetFuelWithFuelsFuel(module))
            .Sum();


    int GetFuelWithFuelsFuel(int mass)
    {
        var fuelNeeded = GetFuelReq(mass);
        var additionalFuel = GetFuelReq(fuelNeeded);
        while (additionalFuel > 0)
        {
            fuelNeeded += additionalFuel;
            additionalFuel = GetFuelReq(additionalFuel);
        }

        return fuelNeeded;
    }

    int GetFuelReq(int mass) => ((int)Math.Floor(mass / 3.0)) - 2;
}