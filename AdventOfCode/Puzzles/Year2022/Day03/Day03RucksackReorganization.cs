using AdventOfCode.Library.Extensions;
using AdventOfCode.Library.Puzzle;

namespace AdventOfCode.Puzzles.Year2022.Day03;

public class Day03RucksackReorganization : Puzzle
{
    private static long Solution1(IEnumerable<string> input)
        => input.Select(line => new Rucksack(line)).ToList()
            .Select(rucksack =>
                rucksack.Compartments[0].Intersect(rucksack.Compartments[1])
            )
            .Sum(duplicate => duplicate.Sum());

    private static long Solution2(IEnumerable<string> input)
        => input.Select(line => new Rucksack(line)).ToList().ToGroupsOfSize(3).Select(group =>
        {
            var result = new List<int>();
            group.ForEach(elf =>
            {
                result = result.IsEmpty()
                    ? elf.CompartmentsJoined()
                    : result.Intersect(elf.CompartmentsJoined()).ToList();
            });

            return result;
        }).Sum(it => it.Sum());

    public override int Year() => 2022;
    public override int Day() => 3;

    public override Dictionary<object, Func<IEnumerable<string>, long>> Solutions() => new()
    {
        { "Part 1", Solution1 },
        { "Part 2", Solution2 }
    };
}