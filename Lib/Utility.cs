using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode;

public class Utility
{
    public static IEnumerable<int> ParseNumberListInput(string input) => from n in input.Split('\n') select int.Parse(n);
}