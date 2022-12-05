using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text.RegularExpressions;
using System.Text;

namespace AdventOfCode.Y2022.Day05;

[ProblemName("Supply Stacks")]
class Solution : Solver
{
    public object PartOne(string input)
    {
        var (stacks, instructions) = Read(input);
        var crates = stacks;
        foreach (var i in instructions)
        {
            crates = ExecuteInstructionSingleMove(crates, i);
        }

        var answer = "";
        foreach (var stack in crates)
        {
            answer += stack.Pop();
        }
        return answer;
    }

    public object PartTwo(string input)
    {
        var (stacks, instructions) = Read(input);
        var crates = stacks;
        foreach (var i in instructions)
        {
            crates = ExecuteInstructionMultiMove(crates, i);
        }

        var answer = "";
        foreach (var stack in crates)
        {
            answer += stack.Pop();
        }
        return answer;
    }

    Stack<char>[] ExecuteInstructionSingleMove(Stack<char>[] crates, Instruction instruction)
    {
        for (var i = 0; i < instruction.count; i++)
        {
            crates[instruction.to - 1].Push(crates[instruction.from - 1].Pop());
        }

        return crates;
    }
    
    Stack<char>[] ExecuteInstructionMultiMove(Stack<char>[] crates, Instruction instruction)
    {
        var movedCrates = new List<char>();
        for (var i = 0; i < instruction.count; i++)
        {
            movedCrates.Add(crates[instruction.from - 1].Pop());
        }

        movedCrates.Reverse();
        for (var i = 0; i < instruction.count; i++)
        {
            crates[instruction.to - 1].Push(movedCrates[i]);
        }

        return crates;
    }

    (Stack<char>[], IEnumerable<Instruction>) Read(string input)
    {
        var splitInput = input.Split("\n\n");
        var parsedCrates = ReadCrates(splitInput[0]);
        var instructions = ReadInstructions(splitInput[1]);
        return (parsedCrates, instructions);
    }

    IEnumerable<Instruction> ReadInstructions(string instructions) =>
        from instr in instructions.Split('\n')
        let nums = new Regex(@"\d+").Matches(instr).Select(x => int.Parse(x.Value)).ToList()
        select new Instruction(nums[0], nums[1], nums[2]);


    Stack<char>[] ReadCrates(string crates)
    {
        var infoRx = new Regex(@"[\d\w]");
        var rows = crates.Split('\n').Reverse();
        var labelRow = rows.First();
        var crateRows = rows.Skip(1);
        return (from stack in infoRx.Matches(labelRow)
                let stackLabel = int.Parse(stack.Value)
                let index = labelRow.IndexOf(stack.Value)
                select new Stack<char>(
                    from cr in crateRows where cr[index] != ' ' select cr[index]
                )
            ).ToArray();
    }
}

record Instruction(int count, int from, int to);