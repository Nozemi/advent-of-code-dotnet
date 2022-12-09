using AdventOfCode.Library.Extensions;
using AdventOfCode.Library.Puzzle;

namespace AdventOfCode.Puzzles.Year2022.Day03;

[Puzzle(Year = 2022, Day = 3)]
public class Day03RucksackReorganization : Puzzle
{
    public Day03RucksackReorganization() : base(
        solutions: new[]
        {
            Solution("Part 1", Solution1),
            Solution("Part 2", Solution2)
        })
    {
    }
    
    private static object Solution1(IEnumerable<string> input)
        => input.Select(line => new Rucksack(line)).ToList()
            .Select(rucksack =>
                rucksack.Compartments[0].Intersect(rucksack.Compartments[1])
            )
            .Sum(duplicate => duplicate.Sum());

    private static object Solution2(IEnumerable<string> input)
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
}