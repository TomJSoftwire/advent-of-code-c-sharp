using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text.RegularExpressions;
using System.Text;
using AdventOfCode.Y2022;
using AdventOfCode.Y2022.Day04;

namespace AdventOfCode.Y2019.Day08;

[ProblemName("Space Image Format")]
class Solution : Solver
{
    public object PartOne(string input)
    {
        var layers = ReadLayers(input);
        var fewestZeros = layers.Min(x => CountOccurences(x, 0));
        var fewestZeroLayer = layers.Where(x => CountOccurences(x,0) == fewestZeros).ToList()[0];

        return CountOccurences(fewestZeroLayer, 1) * CountOccurences(fewestZeroLayer, 2);
    }

    public object PartTwo(string input)
    {
        return 0;
    }

    int CountOccurences(int[][] layer, int occurencesOf) =>
        layer.Select(row => row.Where(x => x == occurencesOf).Count()).Sum();


    IEnumerable<int[][]> ReadLayers(string input)
    {
        var pixelsPerLayer = 25 * 6;
        var layerStrings = input.Chunk(pixelsPerLayer);
        return from layerString in layerStrings
            select (
                from row in layerString.Chunk(25)
                select (
                    from c in row select int.Parse(c.ToString())
                ).ToArray()
            ).ToArray();
    }
}