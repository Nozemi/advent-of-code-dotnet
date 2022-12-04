using AdventOfCode.Library;
using AdventOfCode.Library.Extensions;
using AdventOfCode.Library.Utilities;
using static AdventOfCode.Library.Utilities.VariousUtilities;

namespace AdventOfCode.Puzzles.Year2022.Day04;

public class Day04CampCleanup : Puzzle<List<Elf[]>>
{
    public Day04CampCleanup(bool exampleMode = false) : base(2022, 4, exampleMode)
    {
    }

    public override async Task<List<Elf[]>> ParseInputData()
        => (await RawInput()).Select(group =>
            group.Split(",").Select(elf => new Elf(elf)).ToArray()
        ).ToList();

    public override long SolvePart1(List<Elf[]> parsedInput)
        => parsedInput.Count(group =>
            (group[0].LowestSection <= group[1].LowestSection && group[0].HighestSection >= group[1].HighestSection)
            || (group[1].LowestSection <= group[0].LowestSection && group[1].HighestSection >= group[0].HighestSection)
        );

    public override long SolvePart2(List<Elf[]> parsedInput)
        => parsedInput.Count(group =>
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