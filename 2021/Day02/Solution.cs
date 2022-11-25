using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text.RegularExpressions;
using System.Text;
using AngleSharp.Html.Dom.Events;

namespace AdventOfCode.Y2021.Day02;

enum Direction
{
    up,
    down,
    forward
}

class Position
{
    public int forward { get; init; }
    public int depth { get; init; }
}

[ProblemName("Dive!")]
class Solution : Solver
{
    public object PartOne(string input)
    {
        Position position = GetPositionLinear(Read(input));
        return position.depth * position.forward;
    }

    public object PartTwo(string input)
    {
        Position position = GetPositionAim(Read(input));
        return position.depth * position.forward;
    }

    Position GetPositionAim(IEnumerable<(Direction, int)> input)
    {
        var forward = 0;
        var depth = 0;
        var aim = 0;
        using (IEnumerator<(Direction, int)> dirEnumerator = input.GetEnumerator())
        {
            while (dirEnumerator.MoveNext())
            {
                var (direction, magnitude) = dirEnumerator.Current;
                switch (direction)
                {
                    case Direction.down:
                        aim += magnitude;
                        break;
                    case Direction.forward:
                        forward += magnitude;
                        depth += magnitude * aim;
                        break;
                    case Direction.up:
                        aim -= magnitude;
                        break;
                }
            }
        }

        return
            new Position() { forward = forward, depth = depth };
    }

    Position GetPositionLinear(IEnumerable<(Direction, int)> input)
    {
        var forward = 0;
        var depth = 0;
        using (IEnumerator<(Direction, int)> dirEnumerator = input.GetEnumerator())
        {
            while (dirEnumerator.MoveNext())
            {
                var (direction, magnitude) = dirEnumerator.Current;
                switch (direction)
                {
                    case Direction.down:
                        depth += magnitude;
                        break;
                    case Direction.forward:
                        forward += magnitude;
                        break;
                    case Direction.up:
                        depth -= magnitude;
                        break;
                }
            }
        }

        return
            new Position() { forward = forward, depth = depth };
    }

    IEnumerable<(Direction, int)> Read(string input) =>
        from r in input.Split('\n')
        select ParseRow(r);

    (Direction, int) ParseRow(string input)
    {
        string[] instructions = input.Split(' ');
        Direction direction;
        Direction.TryParse(instructions[0], out direction);

        return (direction, int.Parse(instructions[1]));
    }
}