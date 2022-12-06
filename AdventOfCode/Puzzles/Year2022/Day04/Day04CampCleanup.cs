using AdventOfCode.Library.Extensions;
using AdventOfCode.Library.Puzzle;
using static AdventOfCode.Library.Utilities.VariousUtilities;

namespace AdventOfCode.Puzzles.Year2022.Day04;

public class Day04CampCleanup : Puzzle
{
    private static object Solution1(IEnumerable<string> rawInput) => rawInput.Parse().Count(group =>
        (group[0].LowestSection <= group[1].LowestSection && group[0].HighestSection >= group[1].HighestSection)
        || (group[1].LowestSection <= group[0].LowestSection && group[1].HighestSection >= group[0].HighestSection)
    );
    
    private static object Solution2(IEnumerable<string> rawInput) => rawInput.Parse().Count(group =>
        !IntListFromRange(group[0].LowestSection, group[0].HighestSection)
            .Intersect(IntListFromRange(group[1].LowestSection, group[1].HighestSection)).IsEmpty()
    );

    public override int Year() => 2022;
    public override int Day() => 4;

    public override Dictionary<object, Func<IEnumerable<string>, object>> Solutions() => new()
    {
        { "Part 1", Solution1 },
        { "Part 2", Solution2 }
    };
}

public class Elf
{
    public Elf(string rawElf)
    {
        var ids = rawElf.Split("-")
            .Select(int.Parse)
            .ToList();

        LowestSection = ids.Min();
        HighestSection = ids.Max();
    }

    public int LowestSection { get; }
    public int HighestSection { get; }

    public override string ToString()
        => $"{LowestSection}-{HighestSection}";
}

public static class StringEnumerableExtensions
{
    public static List<Elf[]> Parse(this IEnumerable<string> input)
        => input.Select(group =>
            group.Split(",").Select(elf => new Elf(elf)).ToArray()
        ).ToList();
}