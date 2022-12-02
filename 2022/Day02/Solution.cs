using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text.RegularExpressions;
using System.Text;

namespace AdventOfCode.Y2022.Day02;

[ProblemName("Rock Paper Scissors")]
class Solution : Solver
{
    public object PartOne(string input)
    {
        var games = Read(input);
        var total = games.Aggregate(0, (total, game) => total + ScoreGame(game));
        return total;
    }

    public object PartTwo(string input)
    {
        return 0;
    }

    int ScoreGame((char, char) game)
    {
        var (them, me) = game;
        return me switch
        {
            'X' => 1 + them switch
            {
                'A' => 3,
                'B' => 0,
                'C' => 6,
            },
            'Y' => 2 + them switch
            {
                'A' => 6,
                'B' => 3,
                'C' => 0,
            },
            'Z' => 3 + them switch
            {
                'A' => 0,
                'B' => 6,
                'C' => 3,
            },
        };
    }

    IEnumerable<(char, char)> Read(string input) =>
        from game in input.Split('\n')
        select (game[0], game[2]);
}