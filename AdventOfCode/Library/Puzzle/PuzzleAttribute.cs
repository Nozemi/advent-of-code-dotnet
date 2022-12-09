namespace AdventOfCode.Library.Puzzle;

[AttributeUsage(AttributeTargets.Class)]
public class PuzzleAttribute : Attribute
{
    public int Year { get; init; }
    public int Day { get; init; }
    public string? Title { get; init; }
}