using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
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

record Position(int forward, int depth);

record Instruction(Direction direction, int magnitude);

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

    Position GetPositionAim(IEnumerable<Instruction> input)
    {
        var forward = 0;
        var depth = 0;
        var aim = 0;
        using (IEnumerator<Instruction> dirEnumerator = input.GetEnumerator())
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
            new Position(forward, depth);
    }

    Position GetPositionLinear(IEnumerable<Instruction> input)
    {
        return input.Aggregate(new Position(0, 0), (state, step) => step.direction switch
        {
            Direction.down => state with { depth = state.depth + step.magnitude },
            Direction.forward => state with { forward = state.forward + step.magnitude },
            Direction.up => state with { depth = state.depth - step.magnitude },
            _ => throw new Exception(),
        });
    }

    IEnumerable<Instruction> Read(string input) =>
        from r in input.Split('\n')
        select ParseRow(r);

    Instruction ParseRow(string input)
    {
        string[] instructions = input.Split(' ');
        Direction direction;
        Direction.TryParse(instructions[0], out direction);

        return new Instruction(direction, int.Parse(instructions[1]));
    }
}