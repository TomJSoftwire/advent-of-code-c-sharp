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
        var total = games.Aggregate(0, (total, game) => total + ScoreGameUsingInput(game));
        return total;
    }

    public object PartTwo(string input)
    {
        var games = Read(input);
        var total = games.Aggregate(0, (total, game) => total + ScoreGameToAchieveResult(game));
        return total;
    }

    int ScoreGameUsingInput((char, char) game)
    {
        var (them, me) = game;
        return me switch
        {
            'X' => 1 + them switch
            {
                'A' => 3,
                'B' => 0,
                'C' => 6,
                _ => throw new Exception(),

            },
            'Y' => 2 + them switch
            {
                'A' => 6,
                'B' => 3,
                'C' => 0,
                _ => throw new Exception(),

            },
            'Z' => 3 + them switch
            {
                'A' => 0,
                'B' => 6,
                'C' => 3,
                _ => throw new Exception(),

            },
            _ => throw new Exception(),
        };
    }

    int ScoreGameToAchieveResult((char, char) game)
    {
        var (them, me) = game;
        return me switch
        {
            'X' => 0 + them switch
            {
                'A' => 3,
                'B' => 1,
                'C' => 2,
                _ => throw new Exception(),

            },
            'Y' => 3 + them switch
            {
                'A' => 1,
                'B' => 2,
                'C' => 3,
                _ => throw new Exception(),

            },
            'Z' => 6 + them switch
            {
                'A' => 2,
                'B' => 3,
                'C' => 1,
                _ => throw new Exception(),

            },
            _ => throw new Exception(),

        };
    }

    IEnumerable<(char, char)> Read(string input) =>
        from game in input.Split('\n')
        select (game[0], game[2]);
}