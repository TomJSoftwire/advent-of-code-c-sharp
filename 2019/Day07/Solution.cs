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
        var permutations = GetPermutations(new int[] { 4, 3, 2, 1, 0 }, 5);
        return permutations.Aggregate((double)0, (max, curr) =>
        {
            var result = RunPhaseSequence(curr, input);
            return result > max ? result : max;
        });
    }

    public object PartTwo(string input)
    {
        var permutations = GetPermutations(new int[] { 4, 3, 2, 1, 0 }, 5);
        // var permutations = new int[] { 9, 8, 7, 6, 5 };
        return permutations.Aggregate((double)0, (max, curr) =>
        {
            var computers = BuildFeedbackLoop(input, curr);
            var finalComputer = computers.Last();
            computers.First().inputs.Enqueue(0);
            while (finalComputer.isComplete == false)
            {
                RunLoop(computers);
                Console.WriteLine(finalComputer.outputs.Last());
                if (finalComputer.outputs.Last() == Double.PositiveInfinity)
                {
                    throw new Exception();
                }
            }

            var result = finalComputer.outputs[finalComputer.outputs.Count - 1];
            return result > max ? result : max;

        });
        // return permutations.Aggregate(0, (max, curr) =>
        // {
        //     var result = RunPhaseSequence(curr, program);
        //     return result > max ? result : max;
        // });
    }

    void RunLoop(IEnumerable<Day7PC> computers)
    {
        foreach (var c in computers)
        {
            try
            {
                c.ResumeProgram();
            }
            catch (EmptyInput)
            {
            }
        }
    }

    IEnumerable<Day7PC> BuildFeedbackLoop(string input, IEnumerable<int> phaseSequence)
    {
        var computers = phaseSequence.Select((phase) => new Day7PC(input, phase)).ToList();
        return computers.Select((c, index) =>
        {
            c.outputPc = index == computers.Count() - 1 ? computers[0] : computers[index + 1];
            return c;
        });
    }

    static IEnumerable<IEnumerable<T>>
        GetPermutations<T>(IEnumerable<T> list, int length)
    {
        if (length == 1) return list.Select(t => new T[] { t });

        return GetPermutations(list, length - 1)
            .SelectMany(t => list.Where(e => !t.Contains(e)),
                (t1, t2) => t1.Concat(new T[] { t2 }));
    }

    double RunPhaseSequence(IEnumerable<int> phaseSequence, string program)
        => phaseSequence.Aggregate((double)0, (thrust, phase) =>
        {
            var pc = new Day7PC(program, phase, thrust);
            pc.StartProgram();
            var outputs = pc.outputs;
            return outputs[outputs.Count - 1];
        });
}

class Day7PC : IntCodePC
{
    public Queue<double> inputs = new Queue<double>();
    private int phase;

    public Day7PC outputPc = null;

    public Day7PC(string program, double phase, double input) : base(program)
    {
        this.phase = (int)phase;
        this.inputs.Enqueue(phase);
        this.inputs.Enqueue(input);
    }

    public Day7PC(string program, int phase) : base(program)
    {
        this.phase = phase;
        this.inputs.Enqueue(phase);
    }

    public override double GetInputValue()
    {
        if (this.inputs.Count != 0)
        {
            DebugLog($"Taking {this.inputs.Peek()} from input");
            return this.inputs.Dequeue();
        }

        DebugLog($"Input empty");

        throw new EmptyInput();
    }

    public override void WriteToOutput(double value)
    {
        outputs.Add(value);
        if (this.outputPc != null)
        {
            this.outputPc.inputs.Enqueue(value);
            DebugLog($"sending {value} to phase {this.outputPc.phase}");
        }
    }

    public override void DebugLog(string message)
    {
        var phasedMessage = $"Phase {this.phase}: Index {this.instructionIndex}: {message}";
        base.DebugLog(phasedMessage);
    }
}

class EmptyInput : Exception
{
}