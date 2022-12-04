using AdventOfCode.Library;
using AdventOfCode.Library.Extensions;

namespace AdventOfCode.Puzzles.Year2022.Day03;

public class Day03RucksackReorganization : Puzzle<List<Rucksack>>
{
    public Day03RucksackReorganization(bool example = false) : base(2022, 3, example)
    {
    }

    public override async Task<List<Rucksack>> ParseInputData()
        => (await RawInput()).Select(line => new Rucksack(line)).ToList();

    public override long SolvePart1(List<Rucksack> parsedInput)
        => parsedInput.Select(rucksack =>
            rucksack.Compartments[0].Intersect(rucksack.Compartments[1])
        ).Sum(duplicate => duplicate.Sum());

    public override long SolvePart2(List<Rucksack> parsedInput)
        => parsedInput.ToGroupsOfSize(3).Select(group =>
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