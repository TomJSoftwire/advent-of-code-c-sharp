using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using AdventOfCode.Y2019.Day03;

namespace AdventOfCode.Y2019;

public class IntCodePC
{
    private int SavedRunIndex = 0;
    private List<int> program;
    public List<int> outputs = new List<int>();

    public IntCodePC(string input)
    {
        this.program = Utility.ParseNumberListInput(input, ",").ToList();
    }

    public IntCodePC()
    {
    }

    public List<int> ResumeProgram() => RunProgram(this.program, this.SavedRunIndex, this.outputs);


    public List<int> StartProgram() =>
        RunProgram(this.program, 0, this.outputs);


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
            5 => RunProgram(PerformJumpIfTrue(list, instructionIndex, parameters, out var next), next, outputs),
            6 => RunProgram(PerformJumpIfFalse(list, instructionIndex, parameters, out var next), next, outputs),
            7 => RunProgram(PerformLessThan(list, instructionIndex, parameters), nextInstructionIndex, outputs),
            8 => RunProgram(PerformEqualTo(list, instructionIndex, parameters), nextInstructionIndex, outputs),
            99 => list,
            _ => throw new Exception($"Invalid op-code: {opCode}")
        };
    }

    List<int> PerformEqualTo(List<int> list, int index, List<int> parameters)
    {
        var newList = list.ToList();
        var a = list[parameters[0]];
        var b = list[parameters[1]];
        var writeTo = parameters[2];
        newList[writeTo] = a == b ? 1 : 0;
        return newList;
    }

    List<int> PerformLessThan(List<int> list, int index, List<int> parameters)
    {
        var newList = list.ToList();
        var a = list[parameters[0]];
        var b = list[parameters[1]];
        var writeTo = parameters[2];
        newList[writeTo] = a < b ? 1 : 0;
        return newList;
    }

    public List<int> PerformJumpIfTrue(List<int> list, int index, List<int> parameters, out int nextInstructionIndex)
    {
        var conditionValue = list[parameters[0]];
        var jumpTo = list[parameters[1]];
        nextInstructionIndex = conditionValue == 0 ? index + parameters.Count + 1 : jumpTo;
        return list;
    }

    public List<int> PerformJumpIfFalse(List<int> list, int index, List<int> parameters, out int nextInstructionIndex)
    {
        var conditionValue = list[parameters[0]];
        var jumpTo = list[parameters[1]];
        nextInstructionIndex = conditionValue != 0 ? index + parameters.Count + 1 : jumpTo;
        return list;
    }

    public virtual int GetInputValue()
    {
        return 1;
    }


    public List<int> PerformInput(List<int> list, int index, List<int> parameters)
    {
        var newList = list.ToList();
        var writeLocation = parameters[0];
        newList[writeLocation] = GetInputValue();
        return newList;
    }


    public virtual void WriteToOutput(int value, List<int> outputs)
    {
        outputs.Add(value);
    }
    
    public virtual void WriteToOutput(int value)
    {
        outputs.Add(value);
    }

    public List<int> PerformOutput(List<int> list, int index, List<int> parameters, List<int> outputs)
    {
        WriteToOutput(list[parameters[0]], outputs);
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
            5 => 2,
            6 => 2,
            7 => 3,
            8 => 3,
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