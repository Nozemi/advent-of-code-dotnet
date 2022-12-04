using AdventOfCode.Library.Utilities;
using Microsoft.Extensions.Configuration;
using Serilog;
using static AdventOfCode.Library.Utilities.PuzzleInputLoader;

namespace AdventOfCode.Library;

public interface IPuzzle
{
    int Year();
    int Day();
    string InputFile(bool example = false);
    Task<IEnumerable<string>> RawInput(bool exampleMode = false);
    Task<long> SolvePart1(bool exampleData = false);
    Task<long> SolvePart2(bool exampleData = false);
}

public abstract class Puzzle : IPuzzle
{
    public abstract int Year();
    public abstract int Day();
    private readonly string _token;
    private readonly bool _downloadInput;

    protected Puzzle(IConfiguration config)
    {
        _downloadInput = bool.Parse(config["DownloadInput"] ?? "false");
        _token = config["AoCToken"] ?? throw new Exception("No AoC token was configured");
    }

    public string InputFile(bool example = false)
        => $@"input/{Year()}/day{Day()}{(example ? ".example" : "")}.txt";

    public async Task<IEnumerable<string>> RawInput(bool exampleMode = false)
    {
        while (true)
        {
            if (File.Exists(InputFile(exampleMode)))
                return File.ReadLines(InputFile(exampleMode));

            if (exampleMode || !_downloadInput)
                Log.Warning("Input file does not exist: {File}", InputFile(exampleMode));

            if (!_downloadInput)
                return new List<string>();

            if (await DownloadInput(Year(), Day(), _token, InputFile()))
                continue;

            Log.Error("Something went wrong downloading input: {Input}", InputFile());
            return new List<string>();
        }
    }

    public abstract Task<long> SolvePart1(bool exampleData = false);
    public abstract Task<long> SolvePart2(bool exampleData = false);
}