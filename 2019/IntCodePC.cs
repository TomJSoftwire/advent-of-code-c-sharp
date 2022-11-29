using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Y2019.Day03;

namespace AdventOfCode.Y2019;

public class IntCodePC
{
    public List<int> RunProgram(List<int> list, int instructionIndex, List<int> outputs = null)
    {
        var (opCode, parameterModes) = ParseInstruction(list[instructionIndex]);
        var nextInstructionIndex = instructionIndex + parameterModes.Length + 1;
        var parameters = GetParameterValueLocations(list, instructionIndex, parameterModes);
        // Console.WriteLine($"{opCode}: {parameters}");
        return (opCode) switch
        {
            1 => RunProgram(PerformAdd(list, instructionIndex, parameters), nextInstructionIndex, outputs),
            2 => RunProgram(PerformMultiply(list, instructionIndex, parameters), nextInstructionIndex, outputs),
            3 => RunProgram(PerformInput(list, instructionIndex, parameters), nextInstructionIndex, outputs),
            4 => RunProgram(PerformOutput(list, instructionIndex, parameters, outputs), nextInstructionIndex, outputs),
            99 => list,
            _ => throw new Exception($"Invalid op-code: {opCode}")
        };
    }


    public List<int> PerformInput(List<int> list, int index, List<int> parameters)
    {
        var newList = list.ToList();
        var writeLocation = parameters[0];
        // Console.WriteLine("INPUT REQUIRED");
        // int input = int.Parse(Console.ReadLine());
        newList[writeLocation] = 1;
        return newList;
    }

    public List<int> PerformOutput(List<int> list, int index, List<int> parameters, List<int> outputs)
    {
        Console.WriteLine($"OUTPUT: {list[parameters[0]]}");
        outputs.Add(list[parameters[0]]);
        return list;
    }

    ComputerInstruction ParseInstruction(int instruction)
    {
        int opCode = instruction % 100;
        var instructionString = instruction.ToString();
        var numParameters = opCode switch
        {
            1 => 3,
            2 => 3,
            3 => 1,
            4 => 1,
            _ => 0,
        };
        var parameterModes = new int[numParameters];
        if (instructionString.Length > 2)
        {
            var providedInstructions = instructionString.Substring(0, instructionString.Length - 2)
                .Select(x => int.Parse(x.ToString())).Reverse().ToArray();
            for (var i = 0; i < providedInstructions.Length; i++)
            {
                parameterModes[i] = providedInstructions[i];
            }
        }

        return new ComputerInstruction(opCode, parameterModes);
    }

    List<int> PerformAdd(List<int> list, int startIndex, List<int> paramaters)
    {
        var newList = list.ToList();
        var a = paramaters[0];
        var b = paramaters[1];
        var resultIndex = paramaters[2];
        newList[resultIndex] = list[a] + list[b];
        return newList;
    }

    List<int> PerformMultiply(List<int> list, int startIndex, List<int> paramaters)
    {
        var newList = list.ToList();
        var a = paramaters[0];
        var b = paramaters[1];
        var resultIndex = paramaters[2];
        newList[resultIndex] = list[a] * list[b];
        return newList;
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