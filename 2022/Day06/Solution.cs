using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text.RegularExpressions;
using System.Text;

namespace AdventOfCode.Y2022.Day06;

[ProblemName("Tuning Trouble")]
class Solution : Solver
{
    public object PartOne(string input) => 
        new Communicator(input).FindStartOfSignal(SignalType.Packet);

    public object PartTwo(string input) => 
        new Communicator(input).FindStartOfSignal(SignalType.Message);
}