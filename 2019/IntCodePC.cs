using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Y2019.Day03;

namespace AdventOfCode.Y2019;

public class IntCodePC
{
    public List<int> StartProgram(List<int> list)
    {
        var (opCode, _) = ParseInstruction(list[0]);
        return (opCode) switch
        {
            1 => RunProgram(list, 0, 4),
            2 => RunProgram(list, 0, 4),
            3 => RunProgram(list, 0, 2),
            4 => RunProgram(list, 0, 2),
            99 => list,
            _ => throw new Exception($"Invalid op-code: {opCode}")
        };
    }

    public List<int> RunProgram(List<int> list, int instructionIndex, int numParameters)
    {
        var (opCode, parameterModes) = ParseInstruction(list[instructionIndex], numParameters);
        var nextInstructionIndex = instructionIndex + numParameters;
        return (opCode) switch
        {
            1 => RunProgram(PerformAdd(list, instructionIndex), nextInstructionIndex, 4),
            2 => RunProgram(PerformMultiply(list, instructionIndex), nextInstructionIndex, 4),
            3 => RunProgram(PerformInput(list, instructionIndex), nextInstructionIndex, 2),
            4 => RunProgram(PerformOutput(list, instructionIndex), nextInstructionIndex, 2),
            99 => list,
            _ => throw new Exception($"Invalid op-code: {opCode}")
        };
    }


    public List<int> PerformInput(List<int> list, int index)
    {
        Console.WriteLine("INPUT REQUIRED");
        int input = int.Parse(Console.ReadLine());
        return list;
    }

    public List<int> PerformOutput(List<int> list, int index)
    {
        throw new NotImplementedException();
    }

    ComputerInstruction ParseInstruction(int instruction, int numParameters = 0)
    {
        int opCode = instruction % 100;
        var instructionString = instruction.ToString();
        var parameterModes = new int[numParameters];
        if (instructionString.Length > 2)
        {
            var providedInstructions = instructionString.Substring(0, instructionString.Length - 2).Split()
                .Select(x => int.Parse(x)).Reverse().ToArray();
            for (var i = 0; i < providedInstructions.Length; i++)
            {
                parameterModes[i] = providedInstructions[i];
            }
        }

        return new ComputerInstruction(opCode, parameterModes);
    }

    List<int> PerformAdd(List<int> list, int startIndex)
    {
        var (a, b, resultIndex) = GetValuesAndTarget(list, startIndex + 1);
        list[resultIndex] = a + b;
        return list;
    }

    List<int> PerformMultiply(List<int> list, int startIndex)
    {
        var (a, b, resultIndex) = GetValuesAndTarget(list, startIndex + 1);
        list[resultIndex] = a * b;
        return list;
    }

    ValueIndeces GetValuesAndTarget(List<int> list, int startIndex) =>
        new ValueIndeces(list[list[startIndex]], list[list[startIndex + 1]], list[startIndex + 2]);
}

record ValueIndeces(int a, int b, int result);

record ComputerInstruction(int opCode, int[] parameterModes);