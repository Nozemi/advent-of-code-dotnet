namespace AdventOfCode.Library.Puzzle;

public class PuzzleResult
{
    public PuzzleResult(IPuzzle puzzle)
    {
        Year = puzzle.Year();
        Day = puzzle.Day();
        Name = puzzle.GetType().Name;
    }

    public int Year { get; }
    public int Day { get; }
    public string? Name { get; }
    public Dictionary<string, List<Action<long?>>> Solutions { get; set; } = new();
}