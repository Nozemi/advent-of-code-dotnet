using AdventOfCode.Library.Puzzle;
using AdventOfCode.Library.Utilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace AdventOfCode.Puzzles;

public class PuzzleSolver
{
    private readonly List<IPuzzle> _puzzles;
    private readonly bool _downloadInput;
    private readonly string _token;

    public PuzzleSolver(IServiceProvider provider, IConfiguration configuration)
    {
        _puzzles = provider.GetServices<IPuzzle>()
            .OrderBy(puzzle => puzzle.GetType().Name)
            .ToList();

        _token = configuration["AoCToken"] ?? "";
        _downloadInput = bool.Parse(configuration["DownloadInput"] ?? "false");
    }

    public void FindAndSolvePuzzles()
        => _puzzles.ForEach(Callback);

    private async void Callback(IPuzzle puzzle)
    {
        if (!string.IsNullOrEmpty(_token) && _downloadInput)
            await PuzzleInputLoader.DownloadInput(puzzle.Year(), puzzle.Day(), _token,
                $"input/{puzzle.Year()}/day{puzzle.Day()}.main.txt");

        var chars = "======================";
        for (var i = 0; i < puzzle.GetType().Name.Length; i++) chars += "=";

        Console.WriteLine();
        Log.Information("{Separator}", chars);
        Log.Information("=== Solutions for {Name} ===", puzzle.GetType().Name);
        Log.Information("{Separator}", chars);
        var inputsDirectory = new DirectoryInfo($@"input\\{puzzle.Year()}");
        foreach (var fileInfo in inputsDirectory.GetFiles($"day{puzzle.Day()}*.txt"))
        {
            Log.Information("-- Solutions using input file: {File}", fileInfo.Name);
            foreach (var entry in puzzle.Solutions())
                Log.Information("   {Key}: {Value}", entry.Key, entry.Value(File.ReadLines(fileInfo.FullName)));
        }
    }
}