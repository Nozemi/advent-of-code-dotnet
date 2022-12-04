namespace AdventOfCode.Library.Puzzle;

public interface IPuzzle
{
    int Year();
    int Day();
    Dictionary<object, Func<IEnumerable<string>, long>> Solutions();
}