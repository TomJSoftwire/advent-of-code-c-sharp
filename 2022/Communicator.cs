using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Y2022;

public enum SignalType
{
    Packet,
    Message
}

class CommunicatorConfig
{
    public static Dictionary<SignalType, int> StartOfSignalMarkerLength = new Dictionary<SignalType, int>()
    {
        { SignalType.Packet, 4 },
        { SignalType.Message, 14 },
    };
}

public class Communicator
{
    private string input;
    private int cursorIndex;

    public Communicator(string input)
    {
        this.input = input;
    }

    public int FindStartOfSignal(SignalType signalType)
    {
        var markerLength = CommunicatorConfig.StartOfSignalMarkerLength[signalType];
        var i = 0;
        while (i < input.Length - markerLength)
        {
            var window = input.Substring(i, markerLength);
            var uniqueCharCount = window.Distinct().Count();
            if (uniqueCharCount == markerLength)
            {
                cursorIndex = i + markerLength;
                return i + markerLength;
            }

            i++;
        }

        throw new Exception($"No marker of length {markerLength} found");
    }
}