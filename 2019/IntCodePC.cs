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
            1 => RunProgram(list, 0, 3),
            2 => RunProgram(list, 0, 3),
            3 => RunProgram(list, 0, 1),
            4 => RunProgram(list, 0, 1),
            99 => list,
            _ => throw new Exception($"Invalid op-code: {opCode}")
        };
    }

    public List<int> RunProgram(List<int> list, int instructionIndex, int numParameters)
    {
        var (opCode, parameterModes) = ParseInstruction(list[instructionIndex], numParameters);
        var nextInstructionIndex = instructionIndex + numParameters + 1;
        var parameters = GetParameterValueLocations(list, instructionIndex, parameterModes);
        return (opCode) switch
        {
            1 => RunProgram(PerformAdd(list, instructionIndex, parameters), nextInstructionIndex, 3),
            2 => RunProgram(PerformMultiply(list, instructionIndex, parameters), nextInstructionIndex, 3),
            3 => RunProgram(PerformInput(list, instructionIndex, parameters), nextInstructionIndex, 1),
            4 => RunProgram(PerformOutput(list, instructionIndex, parameters), nextInstructionIndex, 1),
            99 => list,
            _ => throw new Exception($"Invalid op-code: {opCode}")
        };
    }


    public List<int> PerformInput(List<int> list, int index, List<int> parameters)
    {
        Console.WriteLine("INPUT REQUIRED");
        int input = int.Parse(Console.ReadLine());
        return list;
    }

    public List<int> PerformOutput(List<int> list, int index, List<int> parameters)
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

    List<int> PerformAdd(List<int> list, int startIndex, List<int> paramaters)
    {
        var a = paramaters[0];
        var b = paramaters[1];
        var resultIndex = paramaters[2];
        list[resultIndex] = list[a] + list[b];
        return list;
    }

    List<int> PerformMultiply(List<int> list, int startIndex, List<int> paramaters)
    {
        var a = paramaters[0];
        var b = paramaters[1];
        var resultIndex = paramaters[2];
        list[resultIndex] = list[a] * list[b];
        return list;
    }

    List<int> GetParameterValueLocations(List<int> list, int instructionIndex, int[] parameterModes)
    {
        var firstParameterIndex = instructionIndex + 1;
        return parameterModes.Select((mode, index) => mode switch
        {
            1 => firstParameterIndex + index,
            0 => list[firstParameterIndex + index],
            _ => throw new Exception("Missing parameter mode"),
        }).ToList();
    }
}

record ValueIndeces(int a, int b, int result);

record ComputerInstruction(int opCode, int[] parameterModes);