﻿using AdventOfCode.Library.Puzzle;
using AdventOfCode.Library.Utilities;

namespace AdventOfCode.Puzzles.Year2022.Day06;

public class Day06TuningTrouble : Puzzle
{
    private static object Solve(IEnumerable<string> input, int neededDistinctCount)
    {
        var list = input.ToList();
        var index = 0;
        while (list.First().ToList().GetRange(index, neededDistinctCount).Distinct().Count() != neededDistinctCount)
            index++;

        return index + neededDistinctCount;
    }

    private static object Solution1(IEnumerable<string> input) => Solve(input, 4);
    private static object Solution2(IEnumerable<string> input) => Solve(input, 14);

    public override int Year() => 2022;
    public override int Day() => 6;

    public override Dictionary<object, Func<IEnumerable<string>, object>> Solutions() => new()
    {
        {"Part 1", Solution1},
        {"Part 2", Solution2}
    };
}