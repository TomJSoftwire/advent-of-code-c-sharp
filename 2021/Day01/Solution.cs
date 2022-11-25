using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text.RegularExpressions;
using System.Text;

namespace AdventOfCode.Y2021.Day01;

[ProblemName("Sonar Sweep")]
class Solution : Solver
{
    public object PartOne(string input)
    {
        return DepthIncrease(Utility.ParseNumberListInput(input));
    }

    public object PartTwo(string input)
    {
        return DepthIncrease(Get3Window(Utility.ParseNumberListInput(input)));
    }

    int DepthIncrease(IEnumerable<int> ns) => (from p in ns.Zip(ns.Skip(1)) where p.First < p.Second select 1).Count();

    IEnumerable<int> Get3Window(IEnumerable<int> scans) =>
        (from s in scans.Zip(scans.Skip(1), scans.Skip(2)) select s.First + s.Second + s.Third);
}