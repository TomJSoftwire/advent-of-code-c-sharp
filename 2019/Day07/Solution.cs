using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text.RegularExpressions;
using System.Text;

namespace AdventOfCode.Y2019.Day07;

[ProblemName("Amplification Circuit")]
class Solution : Solver
{
    public object PartOne(string input)
    {
        var program = Utility.ParseNumberListInput(input, ",").ToList();
        var permutations = GetPermutations(new int[] { 4, 3, 2, 1, 0 }, 5);
        return permutations.Aggregate(0, (max, curr) =>
        {
            var result = RunPhaseSequence(curr, program);
            return result > max ? result : max;
        });
    }

    public object PartTwo(string input)
    {
        return 0;
    }

    static IEnumerable<IEnumerable<T>>
        GetPermutations<T>(IEnumerable<T> list, int length)
    {
        if (length == 1) return list.Select(t => new T[] { t });

        return GetPermutations(list, length - 1)
            .SelectMany(t => list.Where(e => !t.Contains(e)),
                (t1, t2) => t1.Concat(new T[] { t2 }));
    }

    int RunPhaseSequence(IEnumerable<int> phaseSequence, List<int> program)
        => phaseSequence.Aggregate(0, (thrust, phase) =>
        {
            var pc = new Day7PC(phase, thrust);
            var outputs = new List<int>();
            pc.RunProgram(program, 0, outputs);
            return outputs[outputs.Count - 1];
        });
}

class Day7PC : IntCodePC
{
    private Queue<int> inputs = new Queue<int>();

    public Day7PC(int phase, int input)
    {
        this.inputs.Enqueue(phase);
        this.inputs.Enqueue(input);
    }

    public override int GetInputValue()
    {
        return this.inputs.Dequeue();
    }
}