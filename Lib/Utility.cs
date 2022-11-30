using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode;

public class Utility
{
    
    public static IEnumerable<int> ParseNumberListInput(string input, string breakChar = "\n") =>
        from n in input.Split(breakChar) select int.Parse(n);
    public static IEnumerable<double> ParseDoubleListInput(string input, string breakChar = "\n") =>
        from n in input.Split(breakChar) select double.Parse(n);
}