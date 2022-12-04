using AdventOfCode.Library;
using AdventOfCode.Library.Extensions;
using Microsoft.Extensions.Configuration;
using static AdventOfCode.Library.Utilities.VariousUtilities;

namespace AdventOfCode.Puzzles.Year2022.Day04;

public class Day04CampCleanup : Puzzle
{
    public Day04CampCleanup(IConfiguration config) : base(config)
    {
    }
    
    public override int Year() => 2022;
    public override int Day() => 4;

    private async Task<List<Elf[]>> ParseInputData(bool exampleData = false)
        => (await RawInput(exampleData)).Select(group =>
            group.Split(",").Select(elf => new Elf(elf)).ToArray()
        ).ToList();

    public override async Task<long> SolvePart1(bool exampleData = false)
        => (await ParseInputData(exampleData)).Count(group =>
            (group[0].LowestSection <= group[1].LowestSection && group[0].HighestSection >= group[1].HighestSection)
            || (group[1].LowestSection <= group[0].LowestSection && group[1].HighestSection >= group[0].HighestSection)
        );

    public override async Task<long> SolvePart2(bool exampleData = false)
        => (await ParseInputData(exampleData)).Count(group =>
            !IntListFromRange(group[0].LowestSection, group[0].HighestSection)
                .Intersect(IntListFromRange(group[1].LowestSection, group[1].HighestSection)).IsEmpty()
        );
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