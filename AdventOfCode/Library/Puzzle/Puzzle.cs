namespace AdventOfCode.Library.Puzzle;

public abstract class Puzzle
{
    public IEnumerable<string> RawInput(string inputFile)
        => File.Exists(inputFile)
            ? File.ReadLines(inputFile)
            : new List<string>();

    public Dictionary<object, Func<IEnumerable<string>, object>> Solutions = new();

    protected Puzzle(params Tuple<object, Func<IEnumerable<string>, object>>[] solutions)
    {
        foreach (var solution in solutions)
            Solutions.Add(solution.Item1, solution.Item2);
    }
    
    protected static Tuple<object, Func<IEnumerable<string>, object>> Solution(
        object key, Func<IEnumerable<string>, object> solution
    ) => new(key, solution);
}