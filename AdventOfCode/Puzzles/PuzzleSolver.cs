using AdventOfCode.Library.Extensions;
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
    private readonly bool _downloadExampleInput;
    private readonly List<int>? _daysToRun;
    private readonly List<string>? _inputsToRun;

    public PuzzleSolver(IServiceProvider provider, IConfiguration configuration)
    {
        _puzzles = provider.GetServices<IPuzzle>()
            .OrderBy(puzzle => puzzle.GetType().Name)
            .ToList();

        _token = configuration["AoCToken"] ?? "";
        _downloadInput = bool.Parse(configuration["DownloadInput"] ?? "false");
        _downloadExampleInput = bool.Parse(configuration["DownloadExampleInput"] ?? "true");

        _daysToRun = configuration.GetSection("DaysToRun").Get<List<int>>();
        _inputsToRun = configuration.GetSection("InputsToRun").Get<List<string>>();
    }

    public void FindAndSolvePuzzles()
        => _puzzles.ForEach(Callback);

    private async void Callback(IPuzzle puzzle)
    {
        if (_daysToRun != null && !_daysToRun.IsEmpty() && !_daysToRun.Contains(puzzle.Day()))
        {
            Log.Debug("Skipping Puzzle: {Day}", puzzle.Day());
            return;
        }
        
        if (_downloadInput)
            await PuzzleInputLoader.DownloadExampleInput(puzzle.Year(), puzzle.Day(),
                $"input/{puzzle.Year()}/day{puzzle.Day()}.example.txt");
        
        if (!string.IsNullOrEmpty(_token) && _downloadInput)
            await PuzzleInputLoader.DownloadInput(puzzle.Year(), puzzle.Day(), _token,
                $"input/{puzzle.Year()}/day{puzzle.Day()}.main.txt");

        var chars = "======================";
        for (var i = 0; i < puzzle.GetType().Name.Length; i++) chars += "=";
        
        var inputsDirectory = new DirectoryInfo($@"input\\{puzzle.Year()}");
        var files = inputsDirectory.GetFiles($"day{puzzle.Day()}*.txt");

        if (_inputsToRun != null && !_inputsToRun.IsEmpty())
        {
            files = files.ToList()
                .FindAll(file => _inputsToRun.Any(input => file.Name.Contains(input)))
                .ToArray();
        }

        if (files.IsEmpty())
            return;
        
        Console.WriteLine();
        Log.Information("{Separator}", chars);
        Log.Information("=== Solutions for {Name} ===", puzzle.GetType().Name);
        Log.Information("{Separator}", chars);
        
        foreach (var fileInfo in files)
        {
            Log.Information("-- Solutions using input file: {File}", fileInfo.Name);
            foreach (var entry in puzzle.Solutions())
                Log.Information("   {Key}: {Value}", entry.Key, entry.Value(File.ReadLines(fileInfo.FullName)));
        }
    }
}