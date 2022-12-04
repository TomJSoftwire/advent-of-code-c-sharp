using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text.RegularExpressions;
using System.Text;

namespace AdventOfCode.Y2022.Day04;

[ProblemName("Camp Cleanup")]
class Solution : Solver
{
    public object PartOne(string input)
    {
        var groups = GetGroupRanges(input);
        return groups.Select(g =>
        {
            var gList = g.ToArray();
            if (IsRangeSubsetOfRange(gList[0], gList[1]) || IsRangeSubsetOfRange(gList[1], gList[0]))
            {
                return 1;
            }

            return 0;
        }).Sum();
    }

    public object PartTwo(string input)
    {
        var groups = GetGroupRanges(input);
        return groups.Select(g =>
        {
            var gList = g.ToArray();
            if (DoRangesOverlap(gList[0], gList[1]))
            {
                return 1;
            }

            return 0;
        }).Sum();
    }

    bool IsRangeSubsetOfRange(SectionRange parentRange, SectionRange childRange) =>
        parentRange.max >= childRange.max && parentRange.min <= childRange.min;

    bool DoRangesOverlap(SectionRange rangeA, SectionRange rangeB) =>
        rangeA.max >= rangeB.min && rangeA.min <= rangeB.max
        || rangeB.max >= rangeA.min && rangeB.min <= rangeA.max;

    IEnumerable<IEnumerable<SectionRange>> GetGroupRanges(string input) =>
        input.Split('\n')
            .Select(group => group.Split(',')
                .Select(rangeString =>
                {
                    var range = rangeString.Split('-').Select(int.Parse).ToList();
                    return new SectionRange(range[0], range[1]);
                }));
}

record SectionRange(int min, int max);