using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Numerics;
using System.Text.RegularExpressions;
using System.Text;
using Microsoft.VisualBasic.CompilerServices;

namespace AdventOfCode.Y2019.Day10;

[ProblemName("Monitoring Station")]
class Solution : Solver
{
    public object PartOne(string input)
    {
        var field = ReadField(input);
        var coords = GetAsteroidCoords(field);
        var t = coords.Max(x =>  CountVisibleAsteroids(x, coords));
        return t;
    }

    public object PartTwo(string input)
    {
        return 0;
    }

    int CountVisibleAsteroids(Coord fromAsteroid, IEnumerable<Coord> allAsteroids)
        => allAsteroids.Where(x => x != fromAsteroid)
            .Where(x => CanAsteroidsSeeEachother(fromAsteroid, x, allAsteroids)).Count();

    bool CanAsteroidsSeeEachother(Coord a, Coord b, IEnumerable<Coord> allAsteroids)
    {
        var otherAsteroids = allAsteroids.Where(x => x != a && x != b);
        var dx = a.x - b.x;
        var dy = a.y - b.y;
        if (dx == 0)
        {
            var res = otherAsteroids.Where(o => o.x == a.x && ((o.y > a.y && o.y < b.y) || (o.y < a.y && o.y > b.y)))
                .ToList();
            return !res.Any();
        }

        if (dy == 0)
        {
            var res = otherAsteroids.Where(o => o.y == a.y && ((o.x > a.x && o.x < b.x) || (o.x < a.x && o.x > b.x)))
                .ToList();
            return !res.Any();
        }

        otherAsteroids = otherAsteroids.Where(o => o.x != a.x && o.y != a.y);
        var direction = GetDirection(a, b);
        var distance = Math.Pow((a.x - b.x), 2) + Math.Pow(a.y - b.y, 2);
        var isLeftOfA = b.x < a.x;
        var otherProps =
            otherAsteroids.Select(o =>
                (GetDirection(a, o), Math.Pow((a.x - o.x), 2) + Math.Pow(a.y - o.y, 2), o.x < a.x));
        var blockingAsteroids = otherProps.Where(o =>
            o.Item1 == direction &&
            o.Item2 < distance &&
            o.Item3 == isLeftOfA);
        var result = !blockingAsteroids.Any();
        return result;
    }

    double GetDirection(Coord a, Coord b)
    {
        var dx = (double)(a.x - b.x);
        var dy = (double)(a.y - b.y);
        return (dy / dx);
    }


    IEnumerable<Coord> GetAsteroidCoords(bool[][] field)
    {
        var locs = field.Select(
            (row, y) => row
                .Select((val, x) => val ? new Coord(x, y) : null));
        return locs.SelectMany(x => x).Where(x => x != null);
    }

    bool[][] ReadField(string input) =>
        input.Split('\n').Select(row => row.Select(x => x == '#').ToArray()).ToArray();
}

record Coord(int x, int y);