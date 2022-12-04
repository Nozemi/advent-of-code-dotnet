using AdventOfCode.Library;
using Microsoft.Extensions.Configuration;

namespace AdventOfCode.Puzzles.Year2022.Day01;

public class Day01CalorieCounting : Puzzle
{
    public Day01CalorieCounting(IConfiguration config) : base(config)
    {
    }
    
    public override int Year() => 2022;
    public override int Day() => 1;

    private async Task<List<Elf>> ParseInputData(bool exampleData = false)
    {
        var rawInput = await RawInput(exampleData);
        var elves = new List<Elf>();
        foreach (var line in rawInput)
        {
            var id = elves.FindLast(elf => elf.Id > 0)?.Id ?? 0;

            if (string.IsNullOrWhiteSpace(line) || elves.Count == 0)
                id++;

            var elf = elves.FirstOrDefault(elf => elf.Id == id) ?? new Elf(id);

            if (!string.IsNullOrWhiteSpace(line))
                elf.Calories.Add(int.Parse(line));

            var index = elves.FindIndex(e => e.Id == id);
            if (index >= 0) elves.RemoveAt(index);

            elves.Add(elf);
        }

        return elves;
    }

    public override async Task<long> SolvePart1(bool exampleData = false)
        => (await ParseInputData(exampleData)).MaxBy(elf => elf.Calories.Sum())?.Calories.Sum()
           ?? throw new Exception("No elf found...");

    public override async Task<long> SolvePart2(bool exampleData = false)
    {
        var data = await ParseInputData(exampleData);
        
        return data.OrderByDescending(elf => elf.Calories.Sum())
            .ToList()
            .GetRange(0, Math.Min(data.Count, 3))
            .Sum(elf => elf.Calories.Sum());
    }
}