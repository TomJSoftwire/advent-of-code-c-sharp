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
        var coordsWithVisibleCount = coords.Select(x => (x, GetVisibleAsteroids(x, coords).Count()));
        return coordsWithVisibleCount.Max(x => x.Item2);
    }

    public object PartTwo(string input)
    {
        var field = ReadField(input);
        var coords = GetAsteroidCoords(field);
        var coordsWithVisibleCount = coords.Select(x => (x, GetVisibleAsteroids(x, coords).Count()));
        var t = coordsWithVisibleCount.Max(x => x.Item2);
        var stationLoc = coordsWithVisibleCount.Where(x => x.Item2 == t).First().Item1;
        var visibleAsteroids = GetVisibleAsteroids(stationLoc, coords).ToList();
        visibleAsteroids.Sort((a, b) =>
            CalculateClockwiseAngle(stationLoc, a) < CalculateClockwiseAngle(stationLoc, b) ? -1 : 1
        );
        var dir = visibleAsteroids.Select(x => CalculateClockwiseAngle(stationLoc, x));
        var betAsteroid = visibleAsteroids[199];
        return betAsteroid.x * 100 + betAsteroid.y;
    }

    double CalculateClockwiseAngle(Coord stationCoord, Coord asteroidCoord)
    {
        var yDiff = asteroidCoord.y - stationCoord.y;
        var xDiff = asteroidCoord.x - stationCoord.x;
        var angle = Math.Atan2(yDiff, xDiff) * 180 / Math.PI + 90;
        return angle >= 0 ? angle : angle + 360;
    }


    IEnumerable<Coord> GetVisibleAsteroids(Coord fromAsteroid, IEnumerable<Coord> allAsteroids)
        => allAsteroids.Where(x => x != fromAsteroid)
            .Where(x => CanAsteroidsSeeEachother(fromAsteroid, x, allAsteroids));

    bool CanAsteroidsSeeEachother(Coord a, Coord b, IEnumerable<Coord> allAsteroids)
    {
        var otherAsteroids = allAsteroids.Where(x => x != a && x != b);
        var dx = a.x - b.x;
        var dy = a.y - b.y;
        if (dx == 0)
        {
            return !otherAsteroids.Where(
                    o => o.x == a.x && ((o.y > a.y && o.y < b.y) || (o.y < a.y && o.y > b.y)))
                .Any();
        }

        if (dy == 0)
        {
            return !otherAsteroids.Where(
                    o => o.y == a.y && ((o.x > a.x && o.x < b.x) || (o.x < a.x && o.x > b.x)))
                .Any();
        }

        otherAsteroids = otherAsteroids.Where(o => o.x != a.x && o.y != a.y);

        var direction = GetDirection(a, b);
        var distance = Math.Pow((a.x - b.x), 2) + Math.Pow(a.y - b.y, 2);
        var isLeftOfA = b.x < a.x;

        var blockingAsteroids = otherAsteroids.Where(o =>
            GetDirection(a, o) == direction &&
            Math.Pow((a.x - o.x), 2) + Math.Pow(a.y - o.y, 2) < distance &&
            o.x < a.x == isLeftOfA
        );
        return !blockingAsteroids.Any();
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