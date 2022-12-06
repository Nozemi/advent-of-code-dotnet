namespace AdventOfCode.Library.Puzzle;

public abstract class Puzzle : IPuzzle
{
    public abstract int Year();
    public abstract int Day();
    public abstract Dictionary<object, Func<IEnumerable<string>, object>> Solutions();
    public IEnumerable<string> RawInput(string inputFile)
        => File.Exists(inputFile)
            ? File.ReadLines(inputFile)
            : new List<string>();
}