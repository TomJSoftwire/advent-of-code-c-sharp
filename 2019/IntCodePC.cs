using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using AdventOfCode.Y2019.Day03;

namespace AdventOfCode.Y2019;

public class IntCodePC
{
    public int instructionIndex = 0;
    private List<double> program;
    public List<double> outputs = new List<double>();
    public bool isComplete = false;
    public bool debug = false;

    public IntCodePC(string input)
    {
        this.program = Utility.ParseNumberListInput(input, ",").Select(num => (double)num).ToList();
    }

    public IntCodePC()
    {
    }

    public List<double> ResumeProgram() => RunProgram(this.program, this.instructionIndex, this.outputs);


    public List<double> StartProgram() =>
        RunProgram(this.program, this.instructionIndex, this.outputs);


    public List<double> RunProgram(List<double> list, int instructionIndex, List<double> outputs = null)
    {
        this.instructionIndex = instructionIndex;
        var (opCode, parameterModes) = ParseInstruction((int) list[instructionIndex]);
        var nextInstructionIndex = instructionIndex + parameterModes.Length + 1;
        var parameters = GetParameterValueLocations(list, instructionIndex, parameterModes);
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
            99 => EndProgram(list),
            _ => throw new Exception($"Invalid op-code: {opCode}")
        };
    }

    List<double> EndProgram(List<double> list)
    {
        this.isComplete = true;
        return list;
    }

    List<double> PerformEqualTo(List<double> list, int index, List<int> parameters)
    {
        var newList = list.ToList();
        var a = list[parameters[0]];
        var b = list[parameters[1]];
        var writeTo = parameters[2];
        newList[writeTo] = a == b ? 1 : 0;
        DebugLog($"{a} and {b} equality check, writing {(a == b ? 1 : 0)} to {writeTo}");
        return newList;
    }

    List<double> PerformLessThan(List<double> list, int index, List<int> parameters)
    {
        var newList = list.ToList();
        var a = list[parameters[0]];
        var b = list[parameters[1]];
        var writeTo = parameters[2];
        newList[writeTo] = a < b ? 1 : 0;
        DebugLog($"{a} is less than {b} check, writing {(a < b ? 1 : 0)} to {writeTo}");

        return newList;
    }

    public List<double> PerformJumpIfTrue(List<double> list, int index, List<int> parameters,
        out int nextInstructionIndex)
    {
        var conditionValue = list[parameters[0]];
        var jumpTo = list[parameters[1]];
        nextInstructionIndex = (int)(conditionValue == 0 ? index + parameters.Count + 1 : jumpTo);
        DebugLog(conditionValue != 0
            ? $"Jumping to {jumpTo}, condition was {conditionValue}"
            : $"Jump condition false: {conditionValue}");
        return list;
    }

    public List<double> PerformJumpIfFalse(List<double> list, int index, List<int> parameters,
        out int nextInstructionIndex)
    {
        var conditionValue = list[parameters[0]];
        var jumpTo = list[parameters[1]];
        nextInstructionIndex = (int)(conditionValue != 0 ? index + parameters.Count + 1 : jumpTo);
        DebugLog(conditionValue == 0
            ? $"Jumping to {jumpTo}, condition was {conditionValue}"
            : $"Jump condition true: {conditionValue}");
        return list;
    }

    public virtual double GetInputValue()
    {
        return 1;
    }


    public List<double> PerformInput(List<double> list, int index, List<int> parameters)
    {
        var newList = list.ToList();
        var writeLocation = parameters[0];
        var inputValue = GetInputValue();
        newList[writeLocation] = inputValue;
        DebugLog($"Writing {inputValue} to {writeLocation}");
        return newList;
    }

    public virtual void WriteToOutput(double value)
    {
        outputs.Add(value);
    }

    public List<double> PerformOutput(List<double> list, int index, List<int> parameters, List<double> outputs)
    {
        WriteToOutput(list[parameters[0]]);
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
                .Select(x => double.Parse(x.ToString())).Reverse().ToArray();
            for (var i = 0; i < providedInstructions.Length; i++)
            {
                parameterModes[i] = (int) providedInstructions[i];
            }
        }

        return new ComputerInstruction(opCode, parameterModes);
    }

    List<double> PerformAdd(List<double> list, double startIndex, List<int> paramaters)
    {
        var newList = list.ToList();
        var a = paramaters[0];
        var b = paramaters[1];
        var resultIndex = paramaters[2];
        newList[resultIndex] = list[a] + list[b];
        DebugLog($"Writing {a + b} to {resultIndex}");
        return newList;
    }

    List<double> PerformMultiply(List<double> list, int startIndex, List<int> paramaters)
    {
        var newList = list.ToList();
        var a = paramaters[0];
        var b = paramaters[1];
        var resultIndex = paramaters[2];
        newList[resultIndex] = list[a] * list[b];
        DebugLog($"Writing {a * b} to {resultIndex}");
        return newList;
    }

    List<int> GetParameterValueLocations(List<double> list, int instructionIndex, int[] parameterModes)
    {
        var firstParameterIndex = instructionIndex + 1;
        return parameterModes.Select((mode, index) => mode switch
        {
            1 => firstParameterIndex + index,
            0 => (int) list[firstParameterIndex + index],
            _ => throw new Exception("Missing parameter mode"),
        }).ToList();
    }

    public virtual void DebugLog(string message)
    {
        if (debug)
        {
            Console.WriteLine(message);
        }
    }
}

record ValueIndeces(double a, double b, double result);

record ComputerInstruction(int opCode, int[] parameterModes);