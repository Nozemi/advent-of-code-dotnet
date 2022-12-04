using AdventOfCode.Library.Puzzle;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace AdventOfCode.Puzzles;

public class PuzzleSolver
{
    private readonly List<IPuzzle> _puzzles;

    public PuzzleSolver(IServiceProvider provider)
        => _puzzles = provider.GetServices<IPuzzle>()
            .OrderBy(puzzle => puzzle.GetType().Name)
            .ToList();

    public void FindAndSolvePuzzles()
        => _puzzles.ForEach(puzzle =>
        {
            var chars = "======================";
            for (var i = 0; i < puzzle.GetType().Name.Length; i++)
                chars += "=";
            
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
        });
}