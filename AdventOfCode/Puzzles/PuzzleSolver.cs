using System.Reflection;
using AdventOfCode.Library.Extensions;
using AdventOfCode.Library.Puzzle;
using AdventOfCode.Library.Utilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace AdventOfCode.Puzzles;

public class PuzzleSolver
{
    private readonly List<Puzzle> _puzzles;
    private readonly bool _downloadInput;
    private readonly string _token;
    private readonly bool _downloadExampleInput;
    private readonly List<int>? _daysToRun;
    private readonly List<string>? _inputsToRun;

    public PuzzleSolver(IServiceProvider provider, IConfiguration configuration)
    {
        _puzzles = provider.GetServices<Puzzle>()
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

    private async void Callback(Puzzle puzzle)
    {
        var attribute = puzzle.GetType().GetCustomAttribute<PuzzleAttribute>();
        if (attribute == null) return;
        
        if (_daysToRun != null && !_daysToRun.IsEmpty() && !_daysToRun.Contains(attribute.Day))
        {
            Log.Debug("Skipping Puzzle: {Day}", attribute.Day);
            return;
        }
        
        if (_downloadInput)
            await PuzzleInputLoader.DownloadExampleInput(attribute.Year, attribute.Day,
                $"input/{attribute.Year}/day{attribute.Day}.example.txt");
        
        if (!string.IsNullOrEmpty(_token) && _downloadInput)
            await PuzzleInputLoader.DownloadInput(attribute.Year, attribute.Day, _token,
                $"input/{attribute.Year}/day{attribute.Day}.main.txt");

        var chars = "======================";
        for (var i = 0; i < puzzle.GetType().Name.Length; i++) chars += "=";
        
        var inputsDirectory = new DirectoryInfo($@"input\\{attribute.Year}");
        var files = inputsDirectory.GetFiles($"day{attribute.Day}*.txt");

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
            foreach (var entry in puzzle.Solutions)
                Log.Information("   {Key}: {Value}", entry.Key, entry.Value(File.ReadLines(fileInfo.FullName)));
        }
    }
}