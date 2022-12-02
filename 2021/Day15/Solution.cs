using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text.RegularExpressions;
using System.Text;

namespace AdventOfCode.Y2021.Day15;

[ProblemName("Chiton")]
class Solution : Solver
{
    public object PartOne(string input)
    {
        var (caveMap, shortestPaths, completePaths) = BuildArrays(input);
        var startCoord = new Coord(0, 0);
        var targetCoord = new Coord(caveMap[0].Length, caveMap.Length);
        var neighbours = FindNeighbours(startCoord, targetCoord);
        var unvisitedNeighbours = from n in neighbours where completePaths[n.y][n.x] == false select n;
        
        return 0;
    }

    public object PartTwo(string input)
    {
        return 0;
    }

    public IEnumerable<Coord> FindNeighbours(Coord coord, Coord maxCoord)
    {
        var diffs = new Coord[] { new Coord(0, 1), new Coord(0, -1), new Coord(1, 0), new Coord(-1, 0) };
        return from d in diffs
            let n = new Coord(coord.x + d.x, coord.y + d.y)
            where n.x > 0 && n.x <= maxCoord.x && n.y > 0 && n.y <= maxCoord.y
            select n;
    }

    public Arrays BuildArrays(string input)
    {
        var caveMap = input.Split('\n');
        var yMax = caveMap.Length;
        var xMax = caveMap[0].Length;
        var shortestPaths = Enumerable.Repeat(new int[xMax], yMax).ToArray();
        var completePaths = Enumerable.Repeat(Enumerable.Repeat(false, xMax).ToArray(), yMax).ToArray();
        return new Arrays(caveMap, shortestPaths, completePaths);
    }
}

record Coord(int x, int y);

record Arrays(string[] caveMap, int[][] shortestPaths, bool[][] completePaths);

record Node(int risk, int shortestPathRisk);