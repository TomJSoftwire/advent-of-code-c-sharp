using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Data.Common;
using System.Linq;
using System.Text.RegularExpressions;
using System.Text;
using AngleSharp.Html.Dom.Events;

namespace AdventOfCode.Y2022.Day04;

[ProblemName("Camp Cleanup")]
class Solution : Solver
{
    public object PartOne(string input) =>
        CountGroupsByCondition((gList) =>
                IsRangeSubsetOfRange(gList[0], gList[1])
                || IsRangeSubsetOfRange(gList[1], gList[0])
            , input);


    public object PartTwo(string input) =>
            CountGroupsByCondition((gList) => DoRangesOverlap(gList[0], gList[1]), input);

    int CountGroupsByCondition(Func<SectionRange[], bool> testCondition, string input)
        => (from g in GetGroupRanges(input)
            let gList = g.ToArray()
            where testCondition(gList)
            select g).Count();

    bool IsRangeSubsetOfRange(SectionRange parentRange, SectionRange childRange) =>
        parentRange.max >= childRange.max && parentRange.min <= childRange.min;

    bool DoRangesOverlap(SectionRange rangeA, SectionRange rangeB) =>
        rangeA.max >= rangeB.min && rangeA.min <= rangeB.max;

    IEnumerable<IEnumerable<SectionRange>> GetGroupRanges(string input) =>
        from g in input.Split('\n')
        select (from elf in g.Split(',')
                let range = elf.Split('-').Select(int.Parse).ToList()
                select new SectionRange(range[0], range[1])
            );
}

record SectionRange(int min, int max);