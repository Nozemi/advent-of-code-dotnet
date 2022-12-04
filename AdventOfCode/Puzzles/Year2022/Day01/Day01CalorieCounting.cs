using AdventOfCode.Library.Puzzle;

namespace AdventOfCode.Puzzles.Year2022.Day01;

public class Day01CalorieCounting : Puzzle
{
    private static long Solution1(IEnumerable<string> input)
        => input.Parse().MaxBy(elf => elf.Calories.Sum())?.Calories.Sum()
           ?? throw new Exception("No elf found...");

    private static long Solution2(IEnumerable<string> input)
    {
        var data = input.Parse();

        return data.OrderByDescending(elf => elf.Calories.Sum())
            .ToList()
            .GetRange(0, Math.Min(data.Count, 3))
            .Sum(elf => elf.Calories.Sum());
    }

    public override int Year() => 2022;
    public override int Day() => 1;

    public override Dictionary<object, Func<IEnumerable<string>, long>> Solutions() => new()
    {
        { "Part 1", Solution1 },
        { "Part 2", Solution2 }
    };
}

public static class StringEnumerableExtensions
{
    public static List<Elf> Parse(this IEnumerable<string> input)
    {
        var elves = new List<Elf>();
        foreach (var line in input)
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
}

public class Elf
{
    public Elf(int id)
    {
        Id = id;
    }

    public int Id { get; }
    public List<int> Calories { get; } = new();
}