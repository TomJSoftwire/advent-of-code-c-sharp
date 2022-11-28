using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
using System.Text.RegularExpressions;
using System.Text;
using AdventOfCode.Y2021.Day02;

namespace AdventOfCode.Y2019.Day03;

[ProblemName("Crossed Wires")]
class Solution : Solver
{
    public object PartOne(string input)
    {
        var instructions = from wire in input.Split('\n') select ParseWire(wire);
        var wirePositions = (from instruction in instructions select GetWirePositions(instruction));
        var intersections = new HashSet<Coord>();
        wirePositions.Aggregate(new HashSet<Coord>(),
            (occupiedPositions, currWirePos) =>
            {
                var newIntersections = occupiedPositions.ToHashSet();
                newIntersections.IntersectWith(currWirePos);
                occupiedPositions.UnionWith(currWirePos);
                intersections.UnionWith(newIntersections);
                return occupiedPositions;
            });

        return intersections.Aggregate(99999999, (min, curr) =>
            min > GetManhattanDistance(curr) ? GetManhattanDistance(curr) : min
        );
    }

    public object PartTwo(string input)
    {
        var instructions = from wire in input.Split('\n') select ParseWire(wire);
        var lowestDistance = 9999999;

        instructions.Aggregate(new Dictionary<string, int>(), (lookup, wire) =>
        {
            var end = new Coord(0, 0);
            var wireLength = 0;
            foreach (var instruction in wire)
            {
                var distance = instruction.distance;
                while (distance > 0)
                {
                    end = MoveEnd(end, instruction.direction);
                    wireLength += 1;
                    var key = $"{end.x},{end.y}";
                    if (lookup[key] == null || lookup[key] > wireLength)
                    {
                        lookup[key] = wireLength;
                    }

                    distance -= 1;
                }
            }

            return lookup;
        });
        return 0;
    }

    int GetManhattanDistance(Coord coord) => Math.Abs(coord.x) + Math.Abs(coord.y);

    HashSet<Coord> GetWirePositions(IEnumerable<Instruction> instructions)
    {
        var wire = instructions.Aggregate(new Wire(new HashSet<Coord>(), new Coord(0, 0)), (state, step) =>
        {
            var end = state.end;
            var distance = step.distance;
            while (distance > 0)
            {
                end = MoveEnd(end, step.direction);
                state.positions.Add(end);
                distance -= 1;
            }

            return state with { end = end };
        });
        return wire.positions;
    }


    Coord MoveEnd(Coord end, char dir) => dir switch
    {
        'R' => new Coord(end.x + 1, end.y),
        'D' => new Coord(end.x, end.y - 1),
        'U' => new Coord(end.x, end.y + 1),
        'L' => new Coord(end.x - 1, end.y),
        _ => throw new Exception(),
    };

    IEnumerable<Instruction> ParseWire(string wire)
    {
        var strArr = wire.Split(',');
        return from str in strArr select new Instruction(str[0], int.Parse(str.Substring(1)));
    }
}

record Wire(HashSet<Coord> positions, Coord end);

record Intersection(Coord location, int distance);

record Coord
{
    public int x { get; init; }
    public int y { get; init; }
    public int? shortestDist { get; init; }

    public Coord(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public Coord(int x, int y, int shortestDist)
    {
        this.x = x;
        this.y = y;
        this.shortestDist = shortestDist;
    }
};

record Instruction(char direction, int distance);