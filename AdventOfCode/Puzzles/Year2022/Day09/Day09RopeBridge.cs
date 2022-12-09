using AdventOfCode.Library.Puzzle;

namespace AdventOfCode.Puzzles.Year2022.Day09;

[Puzzle(Year = 2022, Day = 9, Title = "Rope Bridge")]
public class Day09RopeBridge : Puzzle
{
    public Day09RopeBridge() : base(
        solutions: new[]
        {
            Solution("Part 1", Solve1),
            Solution("Part 2", Solve2)
        })
    {
    }

    private static object Solve1(IEnumerable<string> input) => 0;
    private static object Solve2(IEnumerable<string> input) => 0;
}