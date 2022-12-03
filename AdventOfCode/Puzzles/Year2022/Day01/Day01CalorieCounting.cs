using AdventOfCode.Library;

namespace AdventOfCode.Puzzles.Year2022.Day01;

public class Day01CalorieCounting : Puzzle<List<Elf>>
{
    public Day01CalorieCounting(bool exampleMode = false) : base(2022, 1, exampleMode)
    {
    }

    public override async Task<List<Elf>> ParseInputData()
    {
        var rawInput = await RawInput();
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

    public override string SolvePart1(List<Elf> elves)
    {
        var largest = elves.MaxBy(elf => elf.Calories.Sum());
        return largest?.Calories.Sum() + "";
    }

    public override string SolvePart2(List<Elf> elves)
    {
        var top3 = elves.OrderByDescending(elf => elf.Calories.Sum())
            .ToList()
            .GetRange(0, Math.Min(elves.Count, 3));

        return top3.Sum(elf => elf.Calories.Sum()) + "";
    }
}