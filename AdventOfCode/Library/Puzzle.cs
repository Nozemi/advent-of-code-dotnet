using System.Net;

namespace AdventOfCode.Library;

public abstract class Puzzle<T> where T : class
{
    private readonly int _year;
    private readonly int _day;
    private readonly bool _exampleMode;

    protected Puzzle(int year, int day, bool exampleMode = false)
    {
        _year = year;
        _day = day;
        _exampleMode = exampleMode;
    }

    public static string InputFile(int year, int day)
        => $@"input/{year}/day{day}.txt";

    protected async Task<IEnumerable<string>> RawInput()
        => !_exampleMode
            ? File.Exists(InputFile(_year, _day))
                ? File.ReadLines(InputFile(_year, _day))
                : await PuzzleInputLoader.DownloadInput(_year, _day)
            : File.ReadLines($@"input/{_year}/day{_day}.example.txt");


    public abstract Task<T> ParseInputData();

    public virtual string SolvePart1(T parsedInput)
    {
        return "Not implemented";
    }

    public virtual string SolvePart2(T parsedInput)
    {
        return "Not implemented";
    }
}